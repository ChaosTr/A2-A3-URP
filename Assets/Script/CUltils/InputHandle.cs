using ExamineSystem;
using Hertzole.GoldPlayer;
using UnityEngine;

public class InputHandle : MonoBehaviour
{
    private ItemPickup pickItemBehavior => Player.Instance.PickItemBehavior;
    private GameObject CurrentPointingObj => Player.Instance.CurrentPointing;
    private InventorySystem InventorySystem => Player.Instance.InventorySystem;
    private bool isPaused = false;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GoldPlayerController playerController;

    private void Start()
    {
        ExamineUIManager.instance.OnCloseExamineBtnClick = onCloseExamineCall;
    }

    private void Update()
    {
        //left click
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentPointingObj)
            {
                Debug.Log("Hit");
                
                if (CurrentPointingObj.GetComponent<IronBarrelBurner>())
                {
                    Player.Instance.SetMessage("This looks like it use for burning something...");
                }
                if (CurrentPointingObj.GetComponent<FramePlace>())
                {
                    Player.Instance.SetMessage("A frame missing...?");
                }
                if (CurrentPointingObj.GetComponent<Pickable>() is Pickable pickable)
                {
                    pickItemBehavior.PickupItem(pickable.gameObject);
                }
                else if (CurrentPointingObj.GetComponent<IInteract>() is IInteract item)
                {
                    item.Interact();
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            pickItemBehavior.DropItem();
        }

        if (Input.GetKeyDown(ExamineInputManager.instance.interactKey))
        {
            var obj = CurrentPointingObj;
            if (obj) ExamineInteractor.Instance.TryExamineItem(obj);
        }

        if (Input.GetKeyDown(ExamineInputManager.instance.dropKey))
        {
            onCloseExamineCall();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pausePanel.SetActive(isPaused);
                Time.timeScale = 0f;
                playerController.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (!isPaused)
            {
                pausePanel.SetActive(isPaused);
                Time.timeScale = 1f;
                playerController.enabled=true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

#if EQUIP_BY_KEYBOARD
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
#else
        if (Input.mouseScrollDelta.y != 0)
        {
            Debug.Log($"mouse scroll {Input.mouseScrollDelta.y}");
            var current = InventorySystem.CurrentHeldIdx;
            Debug.Log($"index {current}");
            var next = current + (int)(-1 * Input.mouseScrollDelta.y);
            current = next % InventorySystem.Max;
            Debug.Log($"after {current}");
            InventorySystem.Equip(current);
            pickItemBehavior.UpdateEquipment();
        }
#endif
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
}