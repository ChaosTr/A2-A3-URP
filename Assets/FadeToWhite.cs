using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToWhite : MonoBehaviour
{
    public Image fadeImage;  // Reference to the white image
    public CanvasGroup questionPanelGroup; // Reference to the CanvasGroup of the question panel
    public float fadeDuration = 2f;
    public GameObject questionPanel;

    private void Start()
    {
        // Make sure the fade image starts invisible
        fadeImage.color = new Color(1f, 1f, 1f, 0f);

        // Ensure the question panel is invisible initially
        if (questionPanelGroup != null)
        {
            questionPanelGroup.alpha = 0f;
            questionPanelGroup.interactable = false;  // Disable interaction until the fade-in is complete
            questionPanelGroup.blocksRaycasts = false;
        }
    }

    // Call this function to start the fade-in and fade-out sequence
    public void TriggerFadeAndQuestion()
    {
        StartCoroutine(FadeInOutCoroutine());
    }

    private IEnumerator FadeInOutCoroutine()
    {
        // Fade to white (fade-in)
        yield return Fade(0f, 1f);

        // Wait for a moment
        yield return new WaitForSeconds(1f);

        // Display and fade in the question UI
        ShowQuestionUI(true);
        yield return FadeQuestionPanel(0f, 1f);  // Fade-in question panel

        // Unlock the cursor so the player can click the buttons
        UnlockCursor();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timeElapsed = 0f;
        Color currentColor = fadeImage.color;

        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(1f, 1f, 1f, endAlpha);
    }

    public IEnumerator FadeQuestionPanel(float startAlpha, float endAlpha)
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            questionPanelGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        questionPanelGroup.alpha = endAlpha;

        if (endAlpha == 1f)
        {
            questionPanelGroup.interactable = true;  // Enable interaction when fully visible
            questionPanelGroup.blocksRaycasts = true;
        }
        else
        {
            questionPanelGroup.interactable = false;  // Disable interaction when fading out
            questionPanelGroup.blocksRaycasts = false;
        }
    }

    // Show the Yes/No question UI
    public void ShowQuestionUI(bool show)
    {
        
        questionPanel.SetActive(show);
    }

    // Unlock the cursor
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;  // Make the cursor visible
    }
}
