using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingScript : MonoBehaviour
{
    public GameObject cleanBasket;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Debug.Log("Wahing");
            cleanBasket.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

