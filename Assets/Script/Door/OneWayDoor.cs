using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour, IInteract
{
    public Animator anim;
    bool isOpen = false;
    // Start is called before the first frame update
    public void Interact()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
    private void OpenDoor()
    {
        anim.Play("OpenDoor");
        isOpen = true;
        Debug.Log("Open");

    }
    private void CloseDoor()
    {
        anim.Play("CloseDoor");
        isOpen = false;
        Debug.Log("Close");

    }
}
