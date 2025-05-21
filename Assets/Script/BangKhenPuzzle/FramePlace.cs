using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePlace : MonoBehaviour, IInteract
{
    private bool framePlaced;
    private bool picturePlaced;
    private bool isTriggered = false;
    public GameObject theFrame;
    public GameObject thePicture;
    public PuzzleChecker puzzleScript;
    public void Interact()
    {
        var heldItem = Player.Instance.InventorySystem.CurrentHeld;
        if (heldItem == null)
        {
            return;
        }

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
                    picturePlaced = true;
                    Player.Instance.InventorySystem.Remove(heldItem);
                    Destroy(heldItem.GameObject);
                    Player.Instance.PickItemBehavior.UpdateEquipment();
                    thePicture.SetActive(true);
                    Debug.Log("[FramePlace] Picture placed.");
                    break;

            }
        }
    }

    private void Update()
    {
        if (picturePlaced && !isTriggered)
        {
            isTriggered = true;
            this.GetComponent<BoxCollider>().enabled = false;
            puzzleScript.puzzle3Done = true;
        }
    }

}

