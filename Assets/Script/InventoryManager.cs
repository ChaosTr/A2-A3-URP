using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public GameObject item;
        public Image icon;
    }

    public InventorySlot[] slots = new InventorySlot[4];
    public Transform holdPosition;
    public LayerMask pickableLayer;

    private int selectedIndex = -1;
    private GameObject heldObject;

    void Update()
    {
        HandlePickup();
        HandleSlotSwitch();
        HandleDropOrUse();
    }

    void HandlePickup()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            if (Physics.Raycast(ray, out RaycastHit hit, 3f, pickableLayer))
            {
                Pickable pickable = hit.collider.GetComponent<Pickable>();
                if (pickable != null)
                {
                    StoreItem(hit.collider.gameObject);
                }
            }
        }
    }

    void HandleSlotSwitch()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SelectSlot(i);
                break;
            }
        }
    }

    void HandleDropOrUse()
    {
        if (Input.GetKeyDown(KeyCode.Z) && heldObject != null)
        {
            DropItem();
        }
        else if (Input.GetMouseButtonDown(0) && heldObject != null)
        {
            Debug.Log("[Inventory] Used item: " + heldObject.name);
            // Optionally: trigger item's function
        }
    }

    void StoreItem(GameObject obj)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = obj;
                slots[i].icon.enabled = true;
                slots[i].icon.sprite = obj.GetComponent<SpriteRenderer>()?.sprite;

                obj.SetActive(false);
                Debug.Log($"[Inventory] Stored {obj.name} into slot {i + 1}");
                return;
            }
        }

        Debug.Log("[Inventory] No empty slot available.");
    }

    void SelectSlot(int index)
    {
        Debug.Log($"[Inventory] Switched to slot {index + 1}");

        for (int i = 0; i < slots.Length; i++)
        {
            //slots[i].icon.color = new Color(1, 1, 1, 0.4f); // fade
        }

        if (slots[index].item != null)
        {
            if (heldObject != null)
                heldObject.SetActive(false);

            heldObject = slots[index].item;
            heldObject.SetActive(true);
            heldObject.transform.SetParent(holdPosition);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;

            slots[index].icon.color = Color.white;
        }
        else
        {
            if (heldObject != null)
                heldObject.SetActive(false);
            heldObject = null;
        }

        selectedIndex = index;
    }

    void DropItem()
    {
        heldObject.transform.SetParent(null);
        heldObject.transform.position = holdPosition.position + transform.forward;
        heldObject.SetActive(true);

        Debug.Log($"[Inventory] Dropped item: {heldObject.name}");

        slots[selectedIndex].item = null;
        slots[selectedIndex].icon.sprite = null;
        slots[selectedIndex].icon.enabled = false;

        heldObject = null;
    }

    public GameObject GetHeldItem()
    {
        return heldObject;
    }
}
