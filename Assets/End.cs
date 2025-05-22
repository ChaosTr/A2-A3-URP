using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hertzole.GoldPlayer;
using UnityEngine.SceneManagement;


public class End : MonoBehaviour
{
    public FadeInFadeOut fadeScript;
    public GoldPlayerController playerScript;
    public AudioSource busSound;
    public bool isTriggered = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            StartCoroutine(EndScene());
            isTriggered = true;
        }
    }

    private IEnumerator EndScene()
    {
        fadeScript.PassOut();
        playerScript.enabled = false;
        busSound.Play();
        yield return new WaitForSeconds(5f);
        playerScript.enabled = false;

    }
}
