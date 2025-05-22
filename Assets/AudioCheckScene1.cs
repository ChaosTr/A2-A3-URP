using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCheckScene1 : MonoBehaviour
{
    public GameObject finalAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finalAudio.SetActive(true);
        }
    }
}
