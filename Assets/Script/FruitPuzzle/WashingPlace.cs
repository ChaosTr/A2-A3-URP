using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WashingPlace : MonoBehaviour, IInteract
{
    public GameObject cleanBasket; // This will show when a basket is placed

    private bool hasPlacedBasket = false;

    public void Interact()
    {
        var heldItem = Player.Instance.InventorySystem.CurrentHeld;

        if (heldItem != null)
        {
            // Check if it's a Basket
            if (!hasPlacedBasket && heldItem.GameObject.GetComponent<PlaceableFruit>() is PlaceableFruit fruit)
            {
                if (fruit.fruitType == FruitType.dirtyBasket)
                {
                    hasPlacedBasket = true;
                    cleanBasket.SetActive(true); // Show the clean basket
                    Player.Instance.InventorySystem.Remove(heldItem);
                    Destroy(heldItem.GameObject);
                    Player.Instance.PickItemBehavior.UpdateEquipment();
                    this.gameObject.SetActive(false);
                }
            }

            // You can add fruit placing logic after this if needed...
        }
        else
        {
            return;
        }
    }
}
