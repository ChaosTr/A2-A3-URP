using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;

    public void SetItem(Sprite itemSprite)
    {
        icon.sprite = itemSprite;
        icon.enabled = true;
    }

    public void ClearItem()
    {
        icon.sprite = null;
        icon.enabled = false;
    }
}
