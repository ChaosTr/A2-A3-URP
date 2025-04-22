using System.Collections;
using System.Collections.Generic;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class IronBarrelBurner : MonoBehaviour, IInteract
{
    public List<PaperType> requiredPapers = new List<PaperType> { PaperType.Note1, PaperType.Note2, PaperType.Note3 };

    private HashSet<PaperType> collectedPapers = new HashSet<PaperType>();

    public GameObject pile1;
    public GameObject pile2;
    public GameObject pile3;
    public GameObject burnPile;
    private bool canBurn = false;
    private bool hasIgnited = false;
    public GameObject fireEffect;
    public PuzzleChecker puzzleCheckerScript;

    public GoldPlayerController playerScript;
    public SwitchCamera switchCameraScript;
    public LightFlickering lightFlickeringScript;


    public Animator matchBox;
    public Animator matchStick;
    public GameObject matchFire;
    public GameObject matchOnHand;

    void Start()
    {
        pile1.SetActive(false);
        pile2.SetActive(false);
        pile3.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            matchBox.SetBool("Light", true);
            matchStick.SetBool("Light", true);
        }
    }


    public void Interact()
    {
        var heldItem = Player.Instance.InventorySystem.CurrentHeld;

        if (heldItem != null && heldItem.GameObject.GetComponent<BurnablePaper>() is BurnablePaper paper)
        {
            PaperType heldType = paper.paperType;

            if (requiredPapers.Contains(heldType) && !collectedPapers.Contains(heldType))
            {
                collectedPapers.Add(heldType);
                Player.Instance.InventorySystem.Remove(heldItem);
                Destroy(heldItem.GameObject);
                Player.Instance.PickItemBehavior.UpdateEquipment();

                Debug.Log($"[IronBarrel] Burned {heldType}");

                if (heldType == PaperType.Note1)
                {
                    Debug.Log("1");
                    pile1.SetActive(true);

                }

                if (heldType == PaperType.Note2)
                {
                    Debug.Log("2");
                    pile2.SetActive(true);

                }

                if (heldType == PaperType.Note3)
                {
                    Debug.Log("3");
                    pile3.SetActive(true);

                }

                if (collectedPapers.Count == requiredPapers.Count)
                {
                    Debug.Log("[IronBarrel] All papers burned! Puzzle complete.");
                    //OnPuzzleComplete();
                    canBurn = true;
                }
            }
            if (heldType == PaperType.MatchBox && canBurn && !hasIgnited)
            {
                hasIgnited = true;
                Player.Instance.InventorySystem.Remove(heldItem);
                Destroy(heldItem.GameObject);
                Player.Instance.PickItemBehavior.UpdateEquipment();

                Debug.Log("[IronBarrel] Matchbox used - fire ignited!");
                //fireEffect.SetActive(true); // Play fire particles or animation
                matchOnHand.SetActive(true);
                OnPuzzleComplete();
                return;
            }

            /*
            else if (collectedPapers.Contains(heldType))
            {
                Debug.Log("[IronBarrel] You already burned this paper.");
            }
            else
            {
                Debug.Log("[IronBarrel] This paper doesn't belong here.");
            }
            */
        }
        else
        {
            Debug.Log("[IronBarrel] You need to hold a paper to burn it.");
        }
    }

    private void OnPuzzleComplete()
    {
        // Do whatever you want — open door, play cutscene, etc.
        StartCoroutine(AfterBurning());
        StartCoroutine(MatchAnimation());
        Debug.Log("[IronBarrel] Triggering puzzle result!");
        puzzleCheckerScript.puzzle2Done = true;
    }

    IEnumerator AfterBurning()
    {
        lightFlickeringScript.enabled = true;
        burnPile.SetActive(true);
        pile1.SetActive(false);
        pile2.SetActive(false);
        pile3.SetActive(false);

        yield return new WaitForSeconds(3f);
        lightFlickeringScript.enabled = false;
        yield return new WaitForSeconds(10f);
        fireEffect.SetActive(false);
    }

    IEnumerator MatchAnimation()
    {
        StartCoroutine(DisableMovement());
        matchBox.SetBool("Light", true);
        matchStick.SetBool("Light", true);
        yield return new WaitForSeconds(1f);
        matchFire.SetActive(true);

        yield return new WaitForSeconds(1.9f);
        fireEffect.SetActive(true);


    }

    IEnumerator DisableMovement()
    {
        playerScript.Camera.CanLookAround = false;
        playerScript.enabled = false;
        Debug.Log("Stop Now");
        lightFlickeringScript.enabled = true;

        yield return new WaitForSeconds(2.5f);
        Debug.Log("Can Move Again");
        playerScript.Camera.CanLookAround = true;
        playerScript.enabled = true;
        lightFlickeringScript.enabled = false;

        matchOnHand.SetActive(false);
    }
}

