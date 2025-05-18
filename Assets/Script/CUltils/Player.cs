using System;
using ExamineSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private float pickupRange = 1.5f;

    public SwitchCamera CameraBehavior;
    //public ExamineSystem.ExaminableItem ExaminableItem;
    public ItemPickup PickItemBehavior;
    public InventoryDisplay InventoryDisplay;
    public ExamineUIManager uiManager;

    public InventorySystem InventorySystem { get; private set; }

    private Camera currentCam => Player.Instance.CameraBehavior.CurrentCam;


    private void Awake()
    {
        Instance = this;
        InventorySystem = new InventorySystem();
    }
    private void Update()
    {
        Ray ray = currentCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            Pickable pick = hit.collider.GetComponent<Pickable>();
            DoorSystem door = hit.collider.GetComponent<DoorSystem>();
            OneWayDoor oneway = hit.collider.GetComponent<OneWayDoor>();
            bool puzzle = hit.collider.CompareTag("Puzzle");
            bool on;
            //Debug.Log(pick);

            if (pick != null || door != null || oneway != null || puzzle)
            {
                on = true;
                uiManager.HighlightCrosshair(on);
            }
            else
            {
                on = false;
                uiManager.HighlightCrosshair(on);
            }
        }
    }
}
