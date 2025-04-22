using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingWhite : MonoBehaviour
{
    public CanvasGroup whiteFadeGroup;
    public float fadeDuration = 2f;

    public bool faded;

    private void Awake()
    {
        if (whiteFadeGroup != null)
        {
            whiteFadeGroup.alpha = 0;
            whiteFadeGroup.blocksRaycasts = false;
        }
    }

    public void FadeInToWhite()
    {
        StartCoroutine(FadeCanvasGroup(0f, 1f));
    }

    public void FadeOutFromWhite()
    {
        StartCoroutine(FadeCanvasGroup(1f, 0f));
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        whiteFadeGroup.blocksRaycasts = true;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            whiteFadeGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            yield return null;
        }

        whiteFadeGroup.alpha = endAlpha;
        whiteFadeGroup.blocksRaycasts = endAlpha != 0;
        faded = true;
    }
}
