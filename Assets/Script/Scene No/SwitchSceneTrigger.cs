using Hertzole.GoldPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchSceneTrigger : MonoBehaviour
{
    [SerializeField] private GoldPlayerController controller;
    [SerializeField] private FadeInFadeOut fade;
    public TextMeshProUGUI dialouge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BadEnding());
            Debug.Log("detected");
        }

        
    }
    IEnumerator BadEnding()
    {
        yield return new WaitForSeconds(1f);
        
        fade.FadeIn();
        Debug.Log("yo");
        controller.enabled = false;
        //yield return new WaitForSeconds(3f);
        //playaudio source
        //dialouge.text = "Why i can't move...";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
