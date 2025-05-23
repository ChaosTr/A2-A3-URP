using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hertzole.GoldPlayer;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controlPanel;
    public GameObject RUSure;
    public ButtonChange noButton;
    public ButtonChange backButton;
    public ButtonChange resumeButton;

    //public CanvasGroup fadeGroup;
    public float fadeDuration = 0.5f;
    [SerializeField]
    private GoldPlayerController playerController;
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        playerController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        resumeButton.changeBack();
    }

    public void ShowControls()
    {
        controlPanel.SetActive(true); // Show controls after fade-in
    }

    public void HideControls()
    {
        controlPanel.SetActive(false);
        backButton.changeBack(); // Show controls after fade-in
    }

    public void AreUSure()
    {
        RUSure.SetActive(true);
    }

    public void CloseAreUSure()
    {
        RUSure.SetActive(false);
        noButton.changeBack();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
