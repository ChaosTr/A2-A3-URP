using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinhTrigger : MonoBehaviour
{
    [SerializeField]
    private RadioToggle radio;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RadioTrigger());
        }
    }

    IEnumerator RadioTrigger()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        radio.Interact();
        Destroy(gameObject, 0.5f);
    }
}
