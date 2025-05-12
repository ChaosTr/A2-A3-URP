using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBillboard : MonoBehaviour
{
    private Camera mainCam;
    private CanvasGroup canvasGroup;
    public float fadeSpeed = 5f;

    private void Awake()
    {
        mainCam = Camera.main;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward,
                         mainCam.transform.rotation * Vector3.up);
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(1));
    }

    public void FadeOutAndDestroy()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(0, true));
    }
    public void SetVisible(bool visible)
    {
        if (visible)
            FadeIn();
        else
            FadeOutAndDisable();
    }

    private System.Collections.IEnumerator FadeTo(float targetAlpha, bool disable = false)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
            yield return null;
        }

        if (disable && targetAlpha == 0)
            gameObject.SetActive(false);
    }

    public void FadeOutAndDisable()
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(0, true));
    }
}
