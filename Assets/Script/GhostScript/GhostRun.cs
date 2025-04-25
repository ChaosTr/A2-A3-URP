using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRun : MonoBehaviour
{
    public GameObject ghost;
    public AudioSource audioSource;
    private bool isTriggered = false;
    void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            StartCoroutine(RotateGhost());
        }
    }

    IEnumerator RotateGhost()
    {
        isTriggered = true; // Prevent retriggering
        Quaternion initialRotation = ghost.transform.rotation;
        Quaternion peekRotation = Quaternion.Euler(0f, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);

        float duration = 0.5f;
        float elapsed = 0f;

        // Rotate to peek
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            ghost.transform.rotation = Quaternion.Slerp(initialRotation, peekRotation, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(ghost);
    }
}
