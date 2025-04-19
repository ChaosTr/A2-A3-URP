using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay: MonoBehaviour
{
    [SerializeField] private Image[] images;

    public void ViewInventory()
    {
        gameObject.SetActive(true);

        var listItemInInventory = Player.Instance.InventorySystem.Storage;
        foreach (var i in listItemInInventory)
        { 
            //images.display
        }
    }

    public void HideInventory()
    { 
        
    }
}
