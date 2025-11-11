using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform slotPanel;

    public InventorySlot slotPrefab;

    public Sprite dirtSprite;
    public Sprite grassSprite;

    private Inventory inventory;
    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void Update()
    {
        UpdateUI(inventory);
    }

    public void UpdateUI(Inventory myInven)
    {
        foreach (var item in myInven.items)
        {
            switch (item.Key)
            {
                case BlockType.Dirt:
                    slotPrefab.ItemSetting(dirtSprite, $"{myInven.items.}");
                    Instantiate(slotPrefab, slotPanel);
                    break;

                case BlockType.Grass:
                    slotPrefab.ItemSetting(grassSprite, $"{myInven.items.ContainsValue()}");
                    Instantiate(slotPrefab, slotPanel);
                    break;

                case BlockType.Water:
                    
                    break;
            }
        }
    }

}
