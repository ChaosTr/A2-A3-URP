using System.Collections;
using System.Collections.Generic;
using ExamineSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleChecker : MonoBehaviour
{
    public bool puzzle1Done;
    public bool puzzle2Done;
    [SerializeField] private bool isTriggered = false;
    public GameObject talisMan1;
    public GameObject talisMan2;

    //[SerializeField] private DoorSystem doorSystem;
    [SerializeField] private FadeToWhite fadeToWhite;
    private bool firstTriggered = false;
    private bool secondTriggered = false;
    private bool onSpot;
    public Animator sealedDoor;

    void Start()
    {
        puzzle1Done = false;
        puzzle2Done = false;
        //doorSystem.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onSpot = true;
        }
    }
    void Update()
    {
        if (puzzle1Done && !firstTriggered)
        {
            firstTriggered = true;
            GainKinematicBack();

            talisMan1.GetComponent<Rigidbody>().isKinematic = false;

        }
        if (puzzle2Done && !secondTriggered)
        {
            secondTriggered = true;
            GainKinematicBack();
            talisMan2.GetComponent<Rigidbody>().isKinematic = false;
        }
        if (puzzle1Done && puzzle2Done && !isTriggered && onSpot)
        {
            isTriggered = true;
            StartCoroutine(LoadWhiteScreen());
            sealedDoor.SetBool("Open", true);
        }
    }

    private IEnumerator GainKinematicBack()
    {
        yield return new WaitForSeconds(1.5f);
        talisMan1.GetComponent<Rigidbody>().isKinematic = true;
        talisMan2.GetComponent<Rigidbody>().isKinematic = true;
    }

    IEnumerator LoadWhiteScreen()
    {
        Debug.Log("NewScene");
        yield return null;
        //sealedDoor.SetBool("Open", true);

        //yield return new WaitForSeconds(1.4f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); REMEBER add scene in the build Index

    }
}
