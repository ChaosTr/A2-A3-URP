using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransiting : MonoBehaviour
{
    public CanvasGroup panelGroup;
    public Button button1;
    public Button button2;
    public float fadeDuration = 1f;
    bool isFading = false;

    void Start()
    {
        button1.onClick.AddListener(() => StartCoroutine(FadeOutAndLoad("Yes")));
        button2.onClick.AddListener(() => StartCoroutine(FadeOutAndLoad("No")));
    }

    void TriggerSceneChange(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndLoad(sceneName));
        }
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        isFading = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            panelGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        panelGroup.alpha = 0f;

        // Just to be sure, disable interaction
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;

        SceneManager.LoadScene(sceneName);
    }
}
