using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int inventorySize = 8;
    public GameObject inventoryUI;
    public Transform itemSlotParanet;
    public GameObject itemSlotPrefab;

    public List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        CreateInvenSlot();
    }

    public void CreateInvenSlot()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotprefab = Instantiate(itemSlotPrefab, itemSlotParanet);
            InventorySlot slot = slotprefab.GetComponent<InventorySlot>();
            slots.Add(slot);
        }

    }

    public bool AddItem(Block blockAdd, int amount)
    {
        int remainingAmoint = amount;

        foreach (InventorySlot slot in slots)
        {
            if (slot.block != null && slot.block.type == blockAdd.type && slot.amount < blockAdd.maxStack)
            {
                int spaceLeft = blockAdd.maxStack - slot.amount;
                int amountAdd = Mathf.Min(remainingAmoint, spaceLeft);
                slot.AddAmount(amountAdd);

                remainingAmoint -= amountAdd;

                if (remainingAmoint <= 0)
                {
                    return true;
                }
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.block == null)
            {
                int amountAdd = Mathf.Min(remainingAmoint, blockAdd.maxStack);
                slot.ItemSetting(blockAdd, amount);
                remainingAmoint-= amountAdd;

                if (remainingAmoint <= 0)
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(Block item, int amount = 1)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.block == item)
            {
                slot.RemoveAmount(amount);
                return;
            }
        }
    }

    public int GetItemCount(Block item)
    {
        int count = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.block == null)
            {
                count += slot.amount;
            }
        }
        return count;
    }
}
