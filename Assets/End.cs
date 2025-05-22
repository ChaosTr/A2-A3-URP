using System.Collections;
using UnityEngine;
using Hertzole.GoldPlayer;
using UnityEngine.SceneManagement;


public class End : MonoBehaviour
{
    public FadeInFadeOut fadeScript;
    public GoldPlayerController playerScript;
    public AudioSource busSound;
    public bool isTriggered = false;

    public GameObject subtitle;

    void Start()
    {
        StartCoroutine(StartSub());
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            StartCoroutine(EndScene());
            isTriggered = true;
        }
    }

    private IEnumerator StartSub()
    {
        yield return new WaitForSeconds(0.5f);
        subtitle.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        subtitle.SetActive(false);
    }

    private IEnumerator EndScene()
    {
        fadeScript.PassOut();
        playerScript.enabled = false;
        busSound.Play();
        yield return new WaitForSeconds(7f);
        playerScript.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }
}
