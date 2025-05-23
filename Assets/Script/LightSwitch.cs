using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteract
{
    public GameObject onState;
    public GameObject onLight;
    public GameObject offState;
    public GameObject offLight;
    public GameObject lightSource;

    public AudioSource audioSource;

    public bool notClick = false;
    private bool isTriggered = false;

    private void Start()
    {
        UpdateLight();

        if (notClick && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(RandomlyTurnOff());
        }
    }

    public void Interact()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        notClick = !notClick;
        UpdateLight();

        if (notClick && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(RandomlyTurnOff());
        }
    }

    private void UpdateLight()
    {
        onState.SetActive(notClick);
        onLight.SetActive(notClick);

        offState.SetActive(!notClick);
        offLight.SetActive(!notClick);

        lightSource.SetActive(notClick);
    }

    IEnumerator RandomlyTurnOff()
    {
        float random = Random.Range(14f, 20f);
        yield return new WaitForSeconds(random);
        if (Random.value < 0.44f)
        {
            notClick = !notClick;
            UpdateLight();
        }
        isTriggered = false;
    }
}
