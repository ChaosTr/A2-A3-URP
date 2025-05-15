using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    public CanvasGroup group;
    public float fadeSpeed = 5f;

    private Coroutine currentFade;

    void Awake()
    {
        if (group == null)
            group = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        Debug.Log("UIHelper.Show() called!");
        StartFade(1f);
    }

    public void Hide()
    {
        Debug.Log("UIHelper.Hide() called!");
        StartFade(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        Debug.Log($"Starting coroutine to fade to {targetAlpha}");
        currentFade = StartCoroutine(FadeTo(targetAlpha));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        Debug.Log("FadeTo() coroutine has started!");

        float threshold = 0.01f;

        while (Mathf.Abs(group.alpha - targetAlpha) > threshold)
        {
            group.alpha = Mathf.MoveTowards(group.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
            Debug.Log($"Fading... Current alpha: {group.alpha}");
            yield return null;
        }

        group.alpha = targetAlpha;
        group.interactable = targetAlpha > 0.5f;
        group.blocksRaycasts = targetAlpha > 0.5f;

        Debug.Log($"Fade complete at alpha = {group.alpha}");
    }
}

