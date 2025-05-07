using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDIsplay : MonoBehaviour
{
    public InventorySlotUI[] slots; // Assign these in the inspector

    private InventorySystem inventory;

    private void Start()
    {
        inventory = Player.Instance.InventorySystem;
        inventory.OnInventoryChanged += UpdateDisplay;

        UpdateDisplay(); // Initial update
    }

    public void ViewInventory()
    {
        gameObject.SetActive(true);
        UpdateDisplay();
    }

    public void HideInventory()
    {
        gameObject.SetActive(false);
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Storage.Count && inventory.Storage[i] != null)
            {
                var item = inventory.Storage[i];
                Sprite icon = item.GameObject.GetComponent<ItemIcon>()?.icon;
                if (icon != null)
                {
                    slots[i].SetItem(icon);
                }
                else
                {
                    slots[i].ClearItem();
                }
            }
            else
            {
                slots[i].ClearItem();
            }
        }
    }
}


