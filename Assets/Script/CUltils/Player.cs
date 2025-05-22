using System;
using System.Collections;
using System.Diagnostics;
using ExamineSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI messageText;
    [SerializeField]
    private float displayDuration = 2.5f;

    [SerializeField] 
    private float pickupRange = 3f;

    public static Player Instance { get; private set; }
    
    public SwitchCamera CameraBehavior;
    public ItemPickup PickItemBehavior;
    public ExamineUIManager uiManager;

    public InventorySystem InventorySystem { get; private set; }

    private Camera currentCam => Player.Instance.CameraBehavior.CurrentCam;


    private void Awake()
    {
        Instance = this;
        InventorySystem = new InventorySystem();
    }

    public GameObject CurrentPointing { get; private set; }

    private void Update()
    {
        Ray ray = currentCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider)
            {
                CurrentPointing = hit.collider.gameObject;
            }
            Pickable pickable = hit.collider.GetComponent<Pickable>();
            IInteract interactable = hit.collider.GetComponent<IInteract>();
            ExaminableItem examine = hit.collider.GetComponent<ExaminableItem>();
            bool puzzle = hit.collider.CompareTag("Puzzle");
            bool on;
            //Debug.Log(pick);

            if (pickable != null || interactable != null || puzzle || examine != null)
            {
                on = true;
                uiManager.HighlightCrosshair(on);
            }
            else
            {
                on = false;
                uiManager.HighlightCrosshair(on);
                CurrentPointing = null;

            }
        }
        else
        {
            CurrentPointing = null;
        }
    }

    public void SetMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        StopCoroutine(nameof(Hide));
        StartCoroutine(Hide());

        IEnumerator Hide()
        {
            yield return new WaitForSeconds(displayDuration);
            if (messageText != null)
            {
                messageText.gameObject.SetActive(false);
            }
        }
    }
}
