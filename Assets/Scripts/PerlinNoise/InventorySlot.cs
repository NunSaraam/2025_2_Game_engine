using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Inventory inven;
    public Block block;

    public int amount;

    public Image blockImage;
    public Text itemText;
    public GameObject emptySlot;

    public void ItemSetting(Block newBlock, int newAmount)
    {
        block = newBlock;
        amount = newAmount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (block != null)
        {
            blockImage.sprite = block.itemicon;
            blockImage.enabled = true;

            itemText.text = amount > 1 ? amount.ToString() : "";

            if (emptySlot != null)
            {
                emptySlot.SetActive(false);
            }
        }
        else
        {
            blockImage.enabled = false;
            itemText.text = "";

            if (emptySlot != null)
            {
                emptySlot.SetActive(true);
            }
        }
    }

    public void AddAmount(int value)
    {
        amount += value;
        UpdateUI();
    }

    public void RemoveAmount(int value)
    {
        amount -= value;

        if (amount < 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateUI();
        }
    }

    public void ClearSlot()
    {
        block = null;
        amount = 0;
        UpdateUI();
    }
}
