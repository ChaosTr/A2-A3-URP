using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRun : MonoBehaviour
{
    public GameObject ghost;
    //public AudioSource auidio;
    public Transform runPoint;
    //public DoorSystem doorSystem;
    public float moveSpeed;
    public Transform door;
    
    private Collider boxCollider;
    private bool isTriggered = false;

    void Awake()
    {
        
        boxCollider = GetComponent<Collider>();
        //door.transform.rotation = Quaternion.Euler(0, -90f, 0);
        
    }

    void OnTriggerEnter(Collider other ) 
    {
        if (isTriggered) return;
        if (other.CompareTag("Player"))
        {
            //doorSystem.closeDoor();
            StartCoroutine(MoveGhost());
        }

    }

    IEnumerator MoveGhost()
    {
        float duration = Vector3.Distance(ghost.transform.position, runPoint.position) / moveSpeed;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration); // Keeps t from going over 1
        ghost.transform.position = Vector3.Lerp(ghost.transform.position, runPoint.position, t);
        //door.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return null;
        }
        ghost.transform.position = runPoint.transform.position;
        Destroy(ghost);
    }

    
}
