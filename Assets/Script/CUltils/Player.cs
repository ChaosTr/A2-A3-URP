using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public SwitchCamera CameraBehavior;
    public ExamineSystem.ExaminableItem ExaminableItem;
    public ItemPickup PickItemBehavior;
    public InventoryDisplay InventoryDisplay;

    public InventorySystem InventorySystem { get; private set; }


    private void Awake()
    {
        Instance = this;
        InventorySystem = new InventorySystem();
    }
}
