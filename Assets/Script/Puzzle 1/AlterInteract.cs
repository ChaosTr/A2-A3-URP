using Hertzole.GoldPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterInteract : MonoBehaviour, IInteract
{
    public List<FruitType> requiredFruits = new List<FruitType> { FruitType.grape, FruitType.banana }; //FruitType.mangcau
    private HashSet<FruitType> placedFruits = new HashSet<FruitType>();

    private bool platePlaced = false;

    // GameObjects on the altar (set inactive at start)
    public GameObject altarPlate;
    public GameObject grapeOnPlate;
    //public GameObject coconutOnPlate;
    public GameObject bananaOnPlate;

    public PuzzleChecker puzzleCheckerScript;

    [Header("=========Light Flicker Script=========")]
    [SerializeField] private GoldPlayerController controller;
    [SerializeField] private LightFlickering lightFlicker;
    [SerializeField] private LightFlickering lightFlicker1;
    [SerializeField] private LightFlickering lightFlicker2;



    public void Start()
    {
        altarPlate.SetActive(false);
        grapeOnPlate.SetActive(false);
        //coconutOnPlate.SetActive(false);
        bananaOnPlate.SetActive(false);
    }

    public void Interact()
    {
        var heldItem = Player.Instance.InventorySystem.CurrentHeld;

        if (heldItem == null)
        {
            Debug.Log("[Altar] You must hold something to place it.");
            return;
        }

        // ========== PLATE LOGIC ==========
        if (heldItem.GameObject.TryGetComponent(out PlaceablePlate plate))
        {
            if (platePlaced)
            {
                Debug.Log("[Altar] Plate is already placed.");
                return;
            }

            if (!plate.isClean)
            {
                Debug.Log("[Altar] The plate is dirty. Clean it first.");
                return;
            }

            // Place the plate
            platePlaced = true;
            Player.Instance.InventorySystem.Remove(heldItem);
            Destroy(heldItem.GameObject);
            Player.Instance.PickItemBehavior.UpdateEquipment();
            altarPlate.SetActive(true);

            Debug.Log("[Altar] Plate placed.");
            return; // Important: return early so we don't run fruit logic
        }

        // ========== FRUIT LOGIC ==========
        if (heldItem.GameObject.TryGetComponent(out PlaceableFruit fruit))
        {
            if (!platePlaced)
            {
                Debug.Log("[Altar] You must place the plate before offering fruit.");
                return;
            }

            FruitType heldType = fruit.fruitType;

            if (!requiredFruits.Contains(heldType))
            {
                Debug.Log("[Altar] This fruit is not part of the ritual.");
                return;
            }

            if (placedFruits.Contains(heldType))
            {
                Debug.Log("[Altar] You've already placed this fruit.");
                return;
            }

            /*
            if (!fruit.isClean)
            {
                Debug.Log("[Altar] The fruit is dirty. Clean it first.");
                return;
            }
            */

            placedFruits.Add(heldType);
            Player.Instance.InventorySystem.Remove(heldItem);
            Destroy(heldItem.GameObject);
            Player.Instance.PickItemBehavior.UpdateEquipment();
            ActivateFruitOnPlate(heldType);
            Debug.Log($"[Altar] Placed {heldType}.");

            CheckPuzzleCompletion();
            return;
        }

        Debug.Log("[Altar] This item cannot be placed here.");
    }

    private void ActivateFruitOnPlate(FruitType type)
    {
        switch (type)
        {
            case FruitType.grape:
                grapeOnPlate.SetActive(true);
                break;
            /*
            case FruitType.mangcau:
                coconutOnPlate.SetActive(true);
                break;
            */
            case FruitType.banana:
                bananaOnPlate.SetActive(true);
                break;
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (platePlaced && placedFruits.Count == requiredFruits.Count)
        {
            Debug.Log("[Altar] All offerings complete. The ritual is done.");
            OnPuzzleComplete();
        }
    }

    private void OnPuzzleComplete()
    {
        // Trigger your next event: ghost, camera cut, etc.
        Debug.Log("[Altar] Ritual complete! Something responds...");
        StartCoroutine(PuzzleFinished());
        puzzleCheckerScript.puzzle1Done = true;

    }

    IEnumerator PuzzleFinished()
    {
        yield return new WaitForSeconds(2f);
        lightFlicker.enabled = true;
        //controller.enabled = false;
        //humanFigure.SetActive(true);

        yield return new WaitForSeconds(4f);
        lightFlicker.enabled = false;
        //humanFigure.SetActive(false);
        //controller.enabled = true;
        //puzzleCheckerScript.puzzle1Done = true;

    }

}
