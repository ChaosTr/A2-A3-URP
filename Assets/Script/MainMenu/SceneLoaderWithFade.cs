using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderWithFade : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;
    public string sceneToLoad = "GameScene"; // Set your target scene name

    void Start()
    {
        // Start with fadeGroup hidden
        fadeGroup.alpha = 0f;
    }

    public void StartGame()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        // Fade to black
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        // Now load scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
