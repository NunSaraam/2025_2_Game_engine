using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image blockImage;
    public TextMeshProUGUI itemText;

    public void ItemSetting(Sprite itemSprite, string text)
    {
        blockImage.sprite = itemSprite;
        itemText.text = text;
    }
}
