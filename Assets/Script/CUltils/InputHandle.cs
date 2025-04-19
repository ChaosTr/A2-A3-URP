using System;
using UnityEngine;

public class InputHandle: MonoBehaviour
{
    public float pickupRange = 3f;

    private Camera currentCam => Player.Instance.CameraBehavior.CurrentCam;
    private ItemPickup pickItemBehavior => Player.Instance.PickItemBehavior;
    private InventoryDisplay inventoryDisplay => Player.Instance.InventoryDisplay;

    private ActionDecision actionDecision;

    private void Start()
    {
        actionDecision = new ActionDecision();
        actionDecision.ItemPickup = pickItemBehavior;
    }

    private void Update()
    {
        //if (Player.Instance.ExaminableItem.IsInvoking)
        //{
        //    return;
        //}
        //left click
        if (Input.GetMouseButtonDown(0))
        {
            //Raycast
            // Cast a ray from the center of the screen
            Ray ray = currentCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;

            // Raycast to detect objects within pickup range
            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                if (hit.collider)
                {
                    actionDecision.OnMouseLeftClickHit(hit.collider);
                }
            }

        }
        //right click
        else if (Input.GetMouseButtonDown(1))
        {
            actionDecision.OnMouseRightClick();
        }
        //open Inventory
        else if (Input.GetKey(KeyCode.Tab))
        {
            inventoryDisplay?.ViewInventory();
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            Player.Instance.InventorySystem.Equip(0);
            pickItemBehavior.UpdateEquipment();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            Player.Instance.InventorySystem.Equip(0);
            pickItemBehavior.UpdateEquipment();
        }

    }
}
