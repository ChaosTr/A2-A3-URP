using System.Collections;
using System.Collections.Generic;
using Hertzole.GoldPlayer;
using UnityEngine;

public class TriggerGhostEvent : MonoBehaviour
{
    [Header("=========Objects=========")]
    public Light light1, light2;
    public GameObject hiddenWall;
    public Animator doorAnim;
    [Header("=========Script=========")]
    public MovingGhost ghostScript;
    public CameraShaker shake;
    public GoldPlayerController movementScript;
    [Header("=========Audio=========")]
    public AudioSource ghostSound;
    public AudioSource doorSlam;
    [Header("=========Bool=========")]
    private bool hasTriggered = false;

    void Start()
    {
        hiddenWall.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            StartCoroutine(ItsComing());
            movementScript.Movement.canRun = true;
        }
    }

    IEnumerator ItsComing()
    {
        hiddenWall.SetActive(true);

        ghostSound.Play();
        doorSlam.Play();

        yield return new WaitForSeconds(0.5f);
        doorAnim.SetBool("Oops", true);
        shake.Shake();

        light1.enabled = false;
        light2.enabled = false;

        yield return new WaitForSeconds(0.1f);
        ghostScript.isMoving = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
