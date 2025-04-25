using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerRandomSound());
        }
    }

    IEnumerator TriggerRandomSound()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        audioS.Play();
        Destroy(this, 2f);
    }
}
