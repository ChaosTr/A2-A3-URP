using ExamineSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class InputHandle : MonoBehaviour
{
    public float pickupRange = 3f;
    public GameObject inspectPoint;
    //private bool isOn = false;
    [SerializeField]
    private string message;
    [SerializeField]
    private TMPro.TextMeshProUGUI messageText;
    private Camera currentCam => Player.Instance.CameraBehavior.CurrentCam;

    [SerializeField]
    private float displayDuration = 2.5f;
    private ItemPickup pickItemBehavior => Player.Instance.PickItemBehavior;

    private void Start()
    {
        ExamineUIManager.instance.OnCloseExamineBtnClick = onCloseExamineCall;
    }

    private void Update()
    {
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
                FramePlace frame = hit.collider.GetComponent<FramePlace>();
                AlterInteract alter = hit.collider.GetComponent<AlterInteract>();
                IronBarrelBurner barrel = hit.collider.GetComponent<IronBarrelBurner>();
                Debug.Log("Hit");
                if (hit.collider)
                {
                    var collider = hit.collider;
                    if (collider.GetComponent<Pickable>() is Pickable pickable)
                    {
                        pickItemBehavior.PickupItem(pickable.gameObject);
                    }
                    else if (collider.GetComponent<IInteract>() is IInteract item)
                    {
                        item.Interact();
                    }
                    
                }              

               if (frame != null)
               {                    
                   message = "There's one missing...";
                   messageText.text = message;
                   messageText.gameObject.SetActive(true);
                   StopCoroutine(nameof(Hide));
                   StartCoroutine(Hide());
               }

                if (alter != null)
                {
                    message = "Am I suppose to place something on there...?";
                    messageText.text = message;
                    messageText.gameObject.SetActive(true);
                    StopCoroutine(nameof(Hide));
                    StartCoroutine(Hide());
                }

                if (barrel != null)
                {
                    message = "This looks like it use for burning something...";
                    messageText.text = message;
                    messageText.gameObject.SetActive(true);
                    StopCoroutine(nameof(Hide));
                    StartCoroutine(Hide());
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            pickItemBehavior.DropItem();
        }

        ////open Inventory
        //if (Input.GetKey(KeyCode.Tab))
        //{
        //    inventoryDisplay?.ViewInventory();
        //}
        //else if (Input.GetKey(KeyCode.Escape))
        //{
        //    inventoryDisplay?.HideInventory();
        //}

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Player.Instance.InventorySystem.Equip(0);
            pickItemBehavior.UpdateEquipment();
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            Player.Instance.InventorySystem.Equip(1);
            pickItemBehavior.UpdateEquipment();
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            Player.Instance.InventorySystem.Equip(2);
            pickItemBehavior.UpdateEquipment();
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            Player.Instance.InventorySystem.Equip(3);
            pickItemBehavior.UpdateEquipment();
        }

        if (Input.GetKeyDown(ExamineInputManager.instance.interactKey))
        {
            var obj = checkRaycast();
            if (obj) ExamineInteractor.Instance.TryExamineItem(obj);
        }

        if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
        {
            onCloseExamineCall();
        }
    }

    private GameObject checkRaycast()
    {
        Ray ray = currentCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        // Raycast to detect objects within pickup range
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider)
            {
                return hit.collider.gameObject;
            }

        }
        return null;
    }

    private void onCloseExamineCall()
    {
        if (ExamineInteractor.Instance.IsExamining)
        {
            //only lerp when the object is examining not pickup and add to inventory
            //if object is examining is from pick up action, no need to lerp for put it back to old position, we will hide it anyway => reduce useless call from coroutine
            bool needLerp = Player.Instance.InventorySystem.NewAdd == null;
            ExamineInteractor.Instance.TryPutbackExamieItem(needLerp);
            Player.Instance.InventorySystem.HideNewAdd();
        }
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(displayDuration);
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }
}