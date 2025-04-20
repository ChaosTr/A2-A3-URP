using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        pile1.SetActive(false);
        pile2.SetActive(false);
        pile3.SetActive(false);
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
                fireEffect.SetActive(true); // Play fire particles or animation
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
        Debug.Log("[IronBarrel] Triggering puzzle result!");
    }

    IEnumerator AfterBurning()
    {
        yield return new WaitForSeconds(3.5f);
        burnPile.SetActive(true);
        pile1.SetActive(false);
        pile2.SetActive(false);
        pile3.SetActive(false);
    }
}

