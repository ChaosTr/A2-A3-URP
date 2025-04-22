using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    public bool puzzle1Done;
    public bool puzzle2Done;
    [SerializeField] private bool isTriggered = false;
    public GameObject talisMan1;
    public GameObject talisMan2;

    //[SerializeField] private DoorSystem doorSystem;
    [SerializeField] private FadeToWhite fadeToWhite;

    void Start()
    {
        puzzle1Done = false;
        puzzle2Done = false;
        //doorSystem.enabled = false;
    }
    void Update()
    {
        if (puzzle1Done)
        {
            talisMan1.GetComponent<Rigidbody>().isKinematic = false;
        }
        if (puzzle2Done)
        {
            talisMan2.GetComponent<Rigidbody>().isKinematic = false;
        }
        if (puzzle1Done && puzzle2Done && !isTriggered)
        {
            isTriggered = true;
            //doorSystem.enabled = true;
            Debug.Log("Door is open, triggering fade...");
            fadeToWhite.TriggerFadeAndQuestion();
            
            Debug.Log("Door open now");
        }
    }
}
