using UnityEngine;

public class RadioToggle : MonoBehaviour, IInteract
{
    public AudioSource radioSource;
    private bool isOn = false;

    public void Interact()
    {
        isOn = !isOn;

        if (isOn)
        {
            radioSource.Play();
            Debug.Log("[Radio] Turned ON");
        }
        else
        {
            radioSource.Stop();
            Debug.Log("[Radio] Turned OFF");
        }
    }
}