using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteract
{
    private bool isClosed = true;
    public Animator anima;
    public void Interact()
    {
        if (isClosed)
        {
            OpenDrawer();
        }
        else
        {
            CloseDrawer();
        }
    }
    private void OpenDrawer()
    {
        anima.Play("Open");
        isClosed = false;
    }

    private void CloseDrawer()
    {
        anima.Play("Close");
        isClosed = true;
    }
}

