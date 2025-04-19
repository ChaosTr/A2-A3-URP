using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRange = 3f;         // Max distance to pick up an object
    public Transform onHandPos;            // Empty GameObject under the camera where items are held
    public float throwForce;
    public Transform throwPos;

    private GameObject heldObject;         // Reference to the currently held object
    private Camera cam;                    // Reference to the main camera

    private void Start()
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
        // Just add it to inventory — no need to hold it yet
        // add to inventory whenever have slot

        if (Player.Instance.InventorySystem.Add(item))
        {
            //hide object when picked, note: not Destroy because reference will null,
            //safe to destroy when have prefab ref but in small prototype game like this
            //just hide it, show it again when use
            item.gameObject.SetActive(false);
            Debug.Log($"[ItemPickup] Added to inventory: {item.name}");
        }
        else //inventory full
        {
            //do what ever you want here
        }
    }

    public void UpdateEquipment()
    {
        heldObject?.SetActive(false);

        var holding = Player.Instance.InventorySystem.CurrentHeld;
        if (holding == null || holding.GameObject == null)
        {
            heldObject = null;
            return;
        }

        //update holding gameobject
        heldObject = holding.GameObject;
        heldObject.transform.SetParent(onHandPos);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
        //show game object that we hide when pickup
        heldObject.gameObject.SetActive(true);

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
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
}