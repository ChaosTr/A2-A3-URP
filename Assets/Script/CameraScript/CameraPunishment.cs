using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hertzole.GoldPlayer;

public class CameraPunishment : MonoBehaviour
{
    [Header("=====Timer=====")]
    public float safeDuration = 25f;
    public float warningDuration = 5f;
    public float dangerInterval = 10f;
    [Header("=====Cool Down=====")]
    public float camCoolDown = 5f;

    [Header("=====Kill Chance=====")]
    public float killChance = 0.4f;

    [Header("=====GameObject=====")]
    // public GameObject warningUI; // Optional: UI effect during warning
    public Animator handAnim;
    public GameObject deathHand;

    public GameObject Battery;
    public GameObject CameraOverlay;
    [Header("=====Bools=====")]
    private bool isRunning = false;
    private bool hasWarned = false;
    public bool isOnCoolDown = false;
    private Coroutine dangerCoroutine;
    [Header("=====Scripts=====")]
    public SwitchCamera switchCameraScript;
    public CameraShaker cameraShaker;
    public GoldPlayerController movementScript;

    void Start()
    {
        //deathHand.SetActive(false);
    }
    void Update()
    {

        // Start hazard timer when camera is on hand
        if (switchCameraScript.camOnHand && !isRunning)
        {
            dangerCoroutine = StartCoroutine(DangerRoutine());
            Debug.Log("Reset");
        }

        // Stop hazard when camera is off hand
        if (!switchCameraScript.camOnHand && isRunning)
        {
            StopCoroutine(dangerCoroutine);
            ResetHazardState();
        }
    }

    private IEnumerator DangerRoutine()
    {
        isRunning = true;
        yield return new WaitForSeconds(safeDuration);

        // Stage 2: Warning period (25-30s)
        hasWarned = true;
        //TriggerWarning(true);

        // Shake the Camera
        if (cameraShaker != null)
        {
            cameraShaker.Shake();
        }

        yield return new WaitForSeconds(warningDuration);
        //TriggerWarning(false);

        // Stage 3: Danger loop (30s onward)
        while (switchCameraScript.camOnHand)
        {
            yield return new WaitForSeconds(dangerInterval);

            if (Random.value <= killChance)
            {
                Debug.Log("Killed by overusing the camera.");
                StartCoroutine(Punish());
                yield break;
            }
        }

        ResetHazardState();
    }
    /*
    private void TriggerWarning(bool show)
    {
        if (warningUI != null)
        {
            warningUI.SetActive(show);
        }
    }
    */

    private IEnumerator Punish()
    {
        movementScript.enabled = false;

        // Darin mouse control
        float duration = 3f;
        float elapsed = 0f;

        Vector2 originalSensitivity = movementScript.Camera.lookSensitivity;
        Vector2 targetSensitivity = Vector2.zero;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            movementScript.Camera.lookSensitivity = Vector2.Lerp(originalSensitivity, targetSensitivity, elapsed / duration);
            yield return null;
        }

        /*
        // Drain movement speed
        duration = 3f;
        elapsed = 0f;

        // Save the original movement speeds
        MovementSpeeds originalWalk = movementScript.Movement.walkingSpeeds;
        MovementSpeeds originalRun = movementScript.Movement.runSpeeds;

        MovementSpeeds target = new MovementSpeeds(0f, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            movementScript.Movement.walkingSpeeds = new MovementSpeeds(
                Mathf.Lerp(originalWalk.forwardSpeed, target.forwardSpeed, t),
                Mathf.Lerp(originalWalk.sidewaysSpeed, target.sidewaysSpeed, t),
                Mathf.Lerp(originalWalk.backwardsSpeed, target.backwardsSpeed, t)
            );

            movementScript.Movement.runSpeeds = new MovementSpeeds(
                Mathf.Lerp(originalRun.forwardSpeed, target.forwardSpeed, t),
                Mathf.Lerp(originalRun.sidewaysSpeed, target.sidewaysSpeed, t),
                Mathf.Lerp(originalRun.backwardsSpeed, target.backwardsSpeed, t)
            );

            yield return null;
        }
        */

        //fpsCamAnimator.enabled = true;

        yield return new WaitForSeconds(0.5f);
        Debug.Log("What is Happening");
        deathHand.SetActive(true);
        handAnim.SetBool("Gotcha", true);

        yield return new WaitForSeconds(3f);
        StartCoroutine(KillPlayer());
    }

    private IEnumerator KillPlayer()
    {
        if (switchCameraScript.fadeOutScript != null)
        {
            switchCameraScript.fadeOutScript.BlackScreenOut();
        }
        Battery.SetActive(false);
        CameraOverlay.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Scene 2");
    }

    private void ResetHazardState()
    {
        isRunning = false;
        hasWarned = false;
        //TriggerWarning(false);
    }
}

