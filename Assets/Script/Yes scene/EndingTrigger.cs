using Hertzole.GoldPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boxCol;
    [SerializeField] private DoorSystem door;
    [SerializeField] private FadingWhite fade;
    [SerializeField] private GoldPlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (door != null)
        {
            if (door.isOpen)
            {
                boxCol.GetComponent<BoxCollider>().enabled = true;
                Debug.Log(boxCol.GetComponent<Collider>().enabled);
            }
        }

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.enabled = false;
            Debug.Log("Player detected");
            fade.FadeInToWhite();
            //SceneManager.LoadScene("MainMenu");
            StartCoroutine(LoadScene());
            

        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
        fade.FadeOutFromWhite();
    }
}
