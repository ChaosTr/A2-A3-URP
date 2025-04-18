using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [Header("Movement Settings")]
    public float followSpeed = 5f;
    public float maxOffsetX = 2f;
    public float maxOffsetY = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Normalize mouse position to range (-1, 1)
        float normalizedX = (Input.mousePosition.x / Screen.width - 0.5f) * 2f;
        float normalizedY = (Input.mousePosition.y / Screen.height - 0.5f) * 2f;

        // Calculate target offset based on max offsets
        Vector3 targetOffset = new Vector3(normalizedX * maxOffsetX, normalizedY * maxOffsetY, 0f);

        // Lerp from current to target position
        Vector3 targetPosition = initialPosition + targetOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
