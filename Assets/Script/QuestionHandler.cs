using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public FadeToWhite fadeToWhiteScript;  // Reference to the FadeToWhite script
    public CanvasGroup questionPanelGroup; // Reference to the CanvasGroup of the question panel
    public string sceneToLoadYes = "MainMenu";  // Scene to load if Yes is clicked
    public string sceneToLoadNo = "MainMenu";  // Scene to load if No is clicked

    public Button yesButton;
    public Button noButton;

    private void Start()
    {
        // Add listeners to handle button clicks
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
    }

    public void OnYesClicked()
    {
        StartCoroutine(FadeOutAndLoadScene(sceneToLoadYes));
    }

    public void OnNoClicked()
    {
        StartCoroutine(FadeOutAndLoadScene(sceneToLoadNo));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneToLoad)
    {
        // Fade out the question panel
        yield return fadeToWhiteScript.FadeQuestionPanel(1f, 0f);

        // After fading out, load the scene
        SceneManager.LoadScene(sceneToLoad);
    }

}
