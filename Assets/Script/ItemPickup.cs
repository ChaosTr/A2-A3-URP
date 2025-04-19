using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPickup : MonoBehaviour
{
    public float pickupRange = 3f;         // Max distance to pick up an object
    public Transform onHandPos;            // Empty GameObject under the camera where items are held
    public float throwForce;
    public Transform throwPos;

    private GameObject heldObject;         // Reference to the currently held object
    private Camera cam;                    // Reference to the main camera

    void Start()
    {
        cam = Camera.main;                 // Get the main camera at the start
    }

    //void Update()
    //{
    //    if (heldObject == null)
    //    {
    //        // Try to pick up when left mouse is clicked
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            TryPickup();
    //        }
    //    }
    //    else
    //    {
    //        // Drop held item with right mouse click
    //        if (Input.GetMouseButtonDown(1))
    //        {
    //            DropItem();
    //        }
    //    }
    //}

    //void TryPickup()
    //{
    //    // Cast a ray from the center of the screen
    //    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
    //    RaycastHit hit;

    //    // Raycast to detect objects within pickup range
    //    if (Physics.Raycast(ray, out hit, pickupRange))
    //    {
    //        // Check if the object has a Pickable component
    //        Pickable pickable = hit.collider.GetComponent<Pickable>();
    //        if (pickable != null)
    //        {
    //            PickupItem(hit.collider.gameObject);
    //        }
    //    }
    //}

    public void PickupItem(GameObject item)
    {
        //if (heldObject != null)
        //{
        //    Debug.LogWarning("[ItemPickup] Already holding an object. Drop first before picking up.");
        //    return;
        //}
        //heldObject = item;
        //Debug.Log($"[ItemPickup] Picking up item: {item.name}");
        //
        //// Disable physics
        //Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        //if (rb)
        //{
        //    rb.isKinematic = true;
        //    rb.useGravity = false;
        //}
        //
        //Player.Instance.InventorySystem.Add(heldObject);

        // If already holding something on-hand, don't allow pickup
        if (Player.Instance.InventorySystem.CurrentHeld != null)
        {
            Debug.LogWarning("[ItemPickup] Cannot pick up. Already holding an equipped item.");
            return;
        }

        // Just add it to inventory — no need to hold it yet
        Player.Instance.InventorySystem.Add(item);
        Debug.Log($"[ItemPickup] Added to inventory: {item.name}");
    }

    public void ClearHeldObject()
    {
        heldObject = null;
    }

    public void DropItem()
    {
        if (heldObject == null || throwPos == null) return;

        // Move the held object to throw position
        heldObject.transform.position = throwPos.position;

        // Re-enable physics
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            // Throw in the forward direction of the camera
            rb.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        }

        // Detach from any parent
        heldObject.transform.SetParent(null);
        heldObject = null;

        var current = Player.Instance.InventorySystem.CurrentHeld;
        Player.Instance.InventorySystem.Remove(current);
    }

    public void UpdateEquipment()
    {
        var holding = Player.Instance.InventorySystem.CurrentHeld;
        if (holding == null || holding.obj == null)
        {
            //tat gameobject
            if (heldObject != null)
            {
                Destroy(heldObject);
                heldObject = null;
            }
            return;
        }
        //else
        //{
        //    var itemObject = holding.obj;
        //    heldObject = itemObject;
        //    //set transform..
        //
        //    // Attach object to onHandPos
        //    heldObject = holding.obj;
        //    heldObject.transform.SetParent(onHandPos);
        //    heldObject.transform.localPosition = Vector3.zero;
        //    heldObject.transform.localRotation = Quaternion.identity;
        //}
        heldObject = holding.obj;
        heldObject.transform.SetParent(onHandPos);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}
