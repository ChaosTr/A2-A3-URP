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

    public bool notClick = false;

    private void Start()
    {
        onState.SetActive(notClick);
        offState.SetActive(!notClick);
    }

    public void Interact()
    {
        notClick = !notClick;
        onState.SetActive(notClick);
        onLight.SetActive(notClick);

        offState.SetActive(!notClick);
        offLight.SetActive(!notClick);

        lightSource.SetActive(notClick);
    }
}
