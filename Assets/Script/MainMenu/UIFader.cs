using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeDuration = 0.5f;

    public GameObject controlPanel; // The control panel (an Image or any UI group)

    void Start()
    {
        fadeGroup.alpha = 0f;
        controlPanel.SetActive(false);
    }

    public void ShowControls()
    {
        StartCoroutine(FadeInOut(() =>
        {
            controlPanel.SetActive(true); // Show controls after fade-in
        }));
    }

    public void HideControls()
    {
        StartCoroutine(FadeInOut(() =>
        {
            controlPanel.SetActive(false); // Hide controls after fade-in
        }));
    }

    IEnumerator FadeInOut(System.Action midAction)
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        midAction?.Invoke(); // Execute after fully black

        // Fade back to visible
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float from, float to)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = Mathf.Lerp(from, to, timer / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = to;
    }
}
