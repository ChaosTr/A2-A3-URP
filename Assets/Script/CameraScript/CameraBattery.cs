using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraBattery : MonoBehaviour
{
    [Header("=====Battery UI Setup=====")]
    public Image batteryImage;

    //FOR BATTERY LOGIC ONLY
    //public Sprite[] batteryImages; // Array of battery colors for each level
    //public GameObject noMoreCam; // Out of Battery Sprite

    [Header("=====Battery UI Container=====")]
    public GameObject batteryUIContainer;
    public GameObject cameraOverlay;

    [Header("=====Camera Reference=====")]
    public SwitchCamera switchCameraScript;

    [Header("=====Blink Effect=====")]
    private float blinkInterval = 0.5f;
    private Coroutine blinkCoroutine;

    //FOR BATTERY LOGIC
    /*
    [Header("=====Camera Setup=====")]
    private float batteryDrainTimer = 0f;
    public float drainTime;
    private int currentBatteryLevel = 4;
    */

    [Header("=====Bool=====")]
    private bool isCoroutineRunning = false;

    public bool batteryEmpty = false; //For SwitchCam script to work Only
    //private bool is2ndCoroutineRunning = false;

    void Start()
    {
        if (batteryUIContainer != null)
        {
            batteryUIContainer.SetActive(false); // Start hidden
        }
        cameraOverlay.SetActive(false);

        //FOR BATTERY LOGIC
        //UpdateBatteryUI(); 
    }

    void Update()
    {
        // Check if camOnHand is true and the coroutine is not running
        if (switchCameraScript.camOnHand == true && !isCoroutineRunning)
        {
            StartCoroutine(ShowBatteryUIWithDelay());
        }

        if (switchCameraScript.camOnHand == false)
        {
            StartCoroutine(TurnOffUI());
        }

        //FOR BATTERY LOGIC
        /*
        if (currentBatteryLevel <= 0)
        {
            batteryEmpty = true;
        }

        // Drain battery only when camera is active
        if (switchCameraScript.camOnHand == true)
        {
            batteryDrainTimer += Time.deltaTime;

            if (batteryDrainTimer >= drainTime)
            {
                batteryDrainTimer = 0f;
                DecreaseBattery();
            }
        }

        if (switchCameraScript.camOnHand == false && batteryEmpty == false && !is2ndCoroutineRunning)
        {
            is2ndCoroutineRunning = true;
            CheckCamBattery();
        }
        */
    }

    // Coroutine to wait before enabling the battery UI
    private IEnumerator ShowBatteryUIWithDelay()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(1.1f); // Wait for the specified delay
        if (batteryUIContainer != null)
        {
            batteryUIContainer.SetActive(true);
            cameraOverlay.SetActive(true); // Show battery UI after delay

            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkBatteryImage());
            }
        }
        isCoroutineRunning = false;
    }

    private IEnumerator BlinkBatteryImage()
    {
        while (true)
        {
            batteryImage.enabled = !batteryImage.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }


    private IEnumerator TurnOffUI()
    {
        yield return new WaitForSeconds(1.1f);
        batteryUIContainer.SetActive(false);
        cameraOverlay.SetActive(false);

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
            batteryImage.enabled = true; // Ensure it's visible again
        }
    }

    //FOR BATTERY ONLY
    /*
    public void CheckCamBattery()
    {
        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(1.2f);
        noMoreCam.SetActive(false);
    }

    void DecreaseBattery()
    {
        currentBatteryLevel--;

        UpdateBatteryUI();

        if (currentBatteryLevel <= 0)
        {
            currentBatteryLevel = 0;
            batteryEmpty = true;
            Debug.Log("Out of Battery");

            // If battery empty force turn camera off 
            if (switchCameraScript != null)
            {
                Debug.Log("Calling ToggleCamera Coroutine");
                switchCameraScript.camOnHand = false;
                StartCoroutine(switchCameraScript.ToggleCamera());
                StartCoroutine(switchCameraScript.Fading());
            }
            else
            {
                Debug.LogError("switchCameraScript is null!");
            }
        }
    }

    void UpdateBatteryUI()
    {
        if (currentBatteryLevel >= 0 && currentBatteryLevel < batteryImages.Length)
        {
            batteryImage.sprite = batteryImages[currentBatteryLevel];
        }
    }

    // Battery Pickup -> Reset Battery
    public void ResetBattery()
    {
        currentBatteryLevel = 4;
        batteryEmpty = false;
        batteryDrainTimer = 0f;
        UpdateBatteryUI();
    }
    */
}
