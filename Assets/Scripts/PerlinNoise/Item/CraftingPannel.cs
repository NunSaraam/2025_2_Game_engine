using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPannel : MonoBehaviour
{
    public Inventory inventory;
    public List<CraftingRecipe> recipeList;
    public GameObject root;
    public TMP_Text plannedText;
    public Button craftBUtton;
    public Button clearButton;
    public TMP_Text hintText;

    readonly Dictionary<ItemType, int> planned = new();

    bool isOpen;

    private void Start()
    {
        SetOpen(false);
        craftBUtton.onClick.AddListener(DoCraft);
        clearButton.onClick.AddListener(ClearPlanned);
        RefreshPlannedUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetOpen(!isOpen);
        }
    }

    public void SetOpen(bool open)
    {
        isOpen = open;
        if (root)
        {
            root.SetActive(open);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!open)
        {
            ClearPlanned();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void AddPlanned(ItemType type, int count = 1)
    {
        if (!planned.ContainsKey(type))
        {
            planned[type] = 0;
        }

        planned[type] += count;

        RefreshPlannedUI();
        SetHint($"{type} x{count} 추가 완료");
    }

    public void ClearPlanned()
    {
        planned.Clear();
        RefreshPlannedUI();
        SetHint("초기화 완료");
    }

    private void RefreshPlannedUI()
    {
        if (!plannedText)
            return;

        if (planned.Count == 0)
        {
            plannedText.text = "우클릭으로 재료를 추가하세요.";
            return;
        }

        var sb = new StringBuilder();

        foreach (var item in planned)
        {
            sb.AppendLine($"{item.Key} x{item.Value}");
        }
        plannedText.text = sb.ToString();
    }

    private void SetHint(string msg)
    {
        if (hintText)
        {
            hintText.text = msg;
        }
    }

    private void DoCraft()
    {
        if (planned.Count == 0)
        {
            SetHint("재료가 부족합니다.");
            return;
        }

        //인벤 수량 체크
        foreach (var plannedItem in planned)
        {
            if (inventory.GetCount(plannedItem.Key) < plannedItem.Value)
            {
                SetHint($"{plannedItem.Key} 가 부족합니다.");
                return;
            }
        }

        var matchedProduct = FindMach(planned);

        if (matchedProduct == null)
        {
            SetHint("알맞은 레시피가 없습니다.");
            return;
        }

        //재료 소모
        foreach (var itemforConsume in planned)
        {
            inventory.Consume(itemforConsume.Key, itemforConsume.Value);
        }

        //결과물 지급
        foreach (var p in matchedProduct.outputs)
        {
            inventory.Add(p.type, p.count);
        }

        ClearPlanned();

        SetHint($"조합완료 : {matchedProduct.displayName}");
    }

    CraftingRecipe FindMach(Dictionary<ItemType, int> planned)
    {
        foreach (var recipe in recipeList)
        {
            //필요한 재료를 충분히 가지고 있는지
            bool ok = true;
            foreach (var ing in recipe.inputs)
            {
                if (!planned.TryGetValue(ing.type, out int have) || have != ing.count)
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                return recipe;
            }
        }
        return null;
    }
}
