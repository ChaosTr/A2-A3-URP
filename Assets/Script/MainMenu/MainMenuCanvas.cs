using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuCanvas : MonoBehaviour
{
    public Button Play;
    public bool sceneFreeze = false;
    private bool isTriggered = false;
    public Light light1;
    public Light light2;


    public void StartGame()
    {
        StartCoroutine(DelayLittle());
    }

    public void Update()
    {
        if (sceneFreeze && !isTriggered)
        {
            StartCoroutine(ChangeScene());
            isTriggered = true;
        }
    }

    IEnumerator DelayLittle()
    {
        yield return
        light1.enabled = true;
        light2.enabled = true;
        Time.timeScale = 0;
        sceneFreeze = true;
        Debug.Log("freeze");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Time.timeScale = 1;
    }

    IEnumerator ChangeScene()
    {
        Debug.Log("Changing time");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
}
