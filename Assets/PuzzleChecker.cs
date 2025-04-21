using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleChecker : MonoBehaviour
{
    public bool puzzle1Done;
    public bool puzzle2Done;
    private bool isTriggered = false;

    void Start()
    {
        puzzle1Done = false;
        puzzle2Done = false;
    }
    void Update()
    {
        if (puzzle1Done && puzzle2Done && !isTriggered)
        {
            isTriggered = true;
            Debug.Log("Door open now");
        }
    }
}
