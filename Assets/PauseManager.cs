using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hertzole.GoldPlayer;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject controlPanel;
    public GameObject RUSure;

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
    }

    public void ShowControls()
    {
        controlPanel.SetActive(true); // Show controls after fade-in
    }

    public void HideControls()
    {
        controlPanel.SetActive(false); // Show controls after fade-in
    }

    public void BackToMainMenu()
    {
        RUSure.SetActive(true);
    }
}
