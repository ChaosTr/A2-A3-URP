using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterInteract : MonoBehaviour, IInteract
{
    // List of required fruits for the puzzle
    public List<FruitType> requiredFruits = new List<FruitType> { FruitType.apple, FruitType.mangcau, FruitType.banana };

    // Track fruits that have already been placed
    private HashSet<FruitType> placedFruits = new HashSet<FruitType>();

    // Slot transforms for visual placement on the altar (assign in inspector)
    public Transform appleSlot;
    public Transform mangcauSlot;
    public Transform bananaSlot;

    // Called when the player interacts with the altar
    public void Interact()
    {
        var heldItem = Player.Instance.InventorySystem.CurrentHeld;

        if (heldItem != null && heldItem.GameObject.TryGetComponent(out PlaceableFruit fruit))
        {
            // Step 1: Check if the fruit type is required
            if (!requiredFruits.Contains(fruit.fruitType))
            {
                Debug.Log("[Altar] This fruit is not part of the required offering.");
                return;
            }

            // Step 2: Check if it's already placed
            if (placedFruits.Contains(fruit.fruitType))
            {
                Debug.Log("[Altar] You've already placed this type of fruit.");
                return;
            }

            // Step 3: Check if the fruit is clean
            if (!fruit.isClean)
            {
                Debug.Log("[Altar] This fruit is dirty. Clean it before offering.");
                return;
            }

            // Step 4: Get corresponding altar slot
            Transform slot = GetSlotForFruit(fruit.fruitType);
            if (slot != null)
            {
                // Snap fruit to altar slot
                fruit.transform.position = slot.position;
                fruit.transform.rotation = slot.rotation;
                fruit.transform.SetParent(slot);

                // Mark as placed
                placedFruits.Add(fruit.fruitType);

                // Remove from inventory and update held item UI
                Player.Instance.InventorySystem.Remove(heldItem);
                Player.Instance.PickItemBehavior.UpdateEquipment();

                Debug.Log($"[Altar] Placed {fruit.fruitType} on altar.");

                // Step 5: Check if puzzle is complete
                if (placedFruits.Count == requiredFruits.Count)
                {
                    Debug.Log("[Altar] All fruits placed. Puzzle complete!");
                    OnPuzzleComplete();
                }
            }
        }
        else
        {
            Debug.Log("[Altar] You must hold a fruit to place it.");
        }
    }

    // Returns the correct altar slot based on fruit type
    private Transform GetSlotForFruit(FruitType type)
    {
        switch (type)
        {
            case FruitType.apple: return appleSlot;
            case FruitType.mangcau: return mangcauSlot;
            case FruitType.banana: return bananaSlot;
            default: return null;
        }
    }

    // Called when all required fruits are placed
    private void OnPuzzleComplete()
    {
        // TODO: Add your puzzle completion logic (e.g., open door, play sound, summon ghost)
        Debug.Log("[Altar] Ritual complete. Proceed with next sequence.");
    }

}
