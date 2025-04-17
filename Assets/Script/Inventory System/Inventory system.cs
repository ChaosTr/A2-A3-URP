using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorysystem : MonoBehaviour
{
    public GameObject inventory;

    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab) && isOpened!)
        {
            inventory.SetActive(true);
        }

        else if (Input.GetKey(KeyCode.Tab) && isOpened)
        {
            inventory.SetActive(false);
        }
    }
}
