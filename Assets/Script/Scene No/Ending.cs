using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject boxCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boxCol.SetActive(false);
        }
    }
}
