using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePlace : MonoBehaviour, IInteract
{
    private bool framePlaced;
    public GameObject theFrame;
    public GameObject thePicture;
    public void Interact()
    {
        var heldItem = Player.Instance.InventorySystem.CurrentHeld;
        if (heldItem.GameObject.TryGetComponent(out PlaceableFrame frame))
        {
            switch (frame.frameType)
            {
                case FrameType.Frame:
                    framePlaced = true;
                    Player.Instance.InventorySystem.Remove(heldItem);
                    Destroy(heldItem.GameObject);
                    Player.Instance.PickItemBehavior.UpdateEquipment();
                    theFrame.SetActive(true);
                    Debug.Log("[FramePlace] Frame placed.");
                    break;

                case FrameType.Picture:
                    if (!framePlaced)
                    {
                        return;
                    }
                    Player.Instance.InventorySystem.Remove(heldItem);
                    Destroy(heldItem.GameObject);
                    Player.Instance.PickItemBehavior.UpdateEquipment();
                    thePicture.SetActive(true);
                    Debug.Log("[FramePlace] Picture placed.");
                    break;

            }
        }
    }
}

