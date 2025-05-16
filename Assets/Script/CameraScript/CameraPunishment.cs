using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hertzole.GoldPlayer;
using System.Runtime.CompilerServices;

public class CameraPunishment : MonoBehaviour
{
    [Header("=====Timer=====")]
    public float safeDuration = 25f;
    public float warningDuration = 5f;
    public float dangerInterval = 10f;

    [Header("=====Kill Chance=====")]
    public float killChance = 0.4f;

    [Header("=====GameObject=====")]
    // public GameObject warningUI; // Optional: UI effect during warning
    public Animator handAnim;
    public GameObject deathHand;
    public string nextSceneName = "Scene 2.1";

    //public GameObject Battery;
    //public GameObject CameraOverlay;
    [Header("=====Bools=====")]
    private bool isRunning = false;
    private Coroutine dangerCoroutine;
    [Header("=====Scripts=====")]
    public SwitchCamera switchCameraScript;
    public CameraShaker cameraShaker;
    public FadeInFadeOut fadeScript;
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

        if (Input.GetKey(KeyCode.H))
        {
            fadeScript.PassOut();
        }
    }

    private IEnumerator DangerRoutine()
    {
        isRunning = true;
        yield return new WaitForSeconds(safeDuration);

        // Shake the Camera
        if (cameraShaker != null)
        {
            cameraShaker.Shake();
        }

        yield return new WaitForSeconds(warningDuration);

        while (switchCameraScript.camOnHand)
        {
            yield return new WaitForSeconds(dangerInterval);

            if (Random.value <= killChance)
            {
                StartCoroutine(DrainMouse());
                StartCoroutine(DrainMoveSpeed());
                yield break;
            }
        }

        ResetHazardState();
    }

    private IEnumerator DrainMouse()
    {
        // Drain mouse control
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
    }

    private IEnumerator DrainMoveSpeed()
    {
        // Drain movement speed
        float duration = 3f;
        float elapsed = 0f;

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
        StartCoroutine(Punish());
    }

    private IEnumerator Punish()
    {
        movementScript.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("What is Happening");
        deathHand.SetActive(true);
        handAnim.SetBool("Gotcha", true);

        yield return new WaitForSeconds(1.2f);
        StartCoroutine(KillPlayer());
    }

    private IEnumerator KillPlayer()
    {
        if (fadeScript != null)
        {
            fadeScript.PassOut();
        }
        //Battery.SetActive(false);
        //CameraOverlay.SetActive(false);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextSceneName);
    }

    private void ResetHazardState()
    {
        isRunning = false;
    }
}

