using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioS;
    public string message;
    public TMPro.TextMeshProUGUI messageText;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Subtitle());
            audioS.Play();
            GetComponent<BoxCollider>().enabled = false;
            //Destroy(gameObject, 0.5f);
        }
    }

    IEnumerator Subtitle()
    {
        messageText.text = message;
        yield return new WaitForSeconds(waitTime);
        messageText.text = " ";
        Destroy(gameObject, 0.5f);

    }
}
