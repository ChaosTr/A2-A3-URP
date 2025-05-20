using System;
using System.Diagnostics;
using ExamineSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private float pickupRange = 3f;

    public SwitchCamera CameraBehavior;
    //public ExamineSystem.ExaminableItem ExaminableItem;
    public ItemPickup PickItemBehavior;
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
            LightSwitch lightswitch = hit.collider.GetComponent<LightSwitch>();
            ExaminableItem examine = hit.collider.GetComponent<ExaminableItem>();
            bool puzzle = hit.collider.CompareTag("Puzzle");
            bool on;
            //Debug.Log(pick);

            if (pick != null || door != null || oneway != null || lightswitch != null || puzzle || examine != null)
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
