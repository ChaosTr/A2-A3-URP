using System.Collections;
using System.Collections.Generic;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Scene2Animations : MonoBehaviour
{
    public GameObject prayBox;
    public GameObject prayCam;
    public GameObject humanFigure;
    public GameObject player;
    public Animator prayAnimation;

    private bool onSpot = false;
    private bool isTriggered = false;
    public GoldPlayerController movementScript;
    public PuzzleChecker puzzleScript;
    public FadeInFadeOut fadeScript;

    void Start()
    {
        prayCam.SetActive(false);
        humanFigure.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onSpot = true;
            Debug.Log("Touched");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            fadeScript.BlackScreenIn();
        }
        if (onSpot && puzzleScript.puzzle1Done && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(Pray());
            //player.SetActive(false);
            movementScript.enabled = false;
            Debug.Log("PlayAnimation");
            fadeScript.BlackScreenOut();
        }
    }

    IEnumerator Pray()
    {
        //Turn Off player and turn on pray
        yield return new WaitForSeconds(1f);
        prayCam.SetActive(true);
        player.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        fadeScript.BlackScreenIn();

        //Play Animation
        yield return new WaitForSeconds(1.5f);
        prayAnimation.SetBool("Pray", true);

        // Turn on the HumanFigure and teleprot player to location
        yield return new WaitForSeconds(6.2f);
        Vector3 targetPosition = new Vector3(-1.884f, 0.2829998f, -3.767f);
        player.transform.position = targetPosition;
        humanFigure.SetActive(true);

        yield return new WaitForSeconds(1.9f);
        humanFigure.SetActive(false);

        //Turn off pray + HumanFigure, turn on player again
        yield return new WaitForSeconds(5f);
        prayCam.SetActive(false);
        player.SetActive(true);
        movementScript.enabled = true;

    }

}


