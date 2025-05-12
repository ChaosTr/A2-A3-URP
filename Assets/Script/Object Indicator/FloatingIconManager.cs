using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIconManager : MonoBehaviour
{
    public float detectionRadius = 3f;
    public LayerMask interactableLayers;

    private Dictionary<GameObject, FloatingBillboard> iconMap = new();

    private void Start()
    {
        // Find all children with FloatingBillboard under interactables
        var interactables = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in interactables)
        {
            var go = obj.gameObject;

            if (go.GetComponent<Pickable>() || go.GetComponent<IInteract>() != null)
            {
                var icon = go.GetComponentInChildren<FloatingBillboard>(true); // include inactive
                if (icon != null)
                {
                    icon.gameObject.SetActive(false); // start off
                    iconMap[go] = icon;
                }
            }
        }
    }

    private void Update()
    {
        foreach (var pair in iconMap)
        {
            GameObject target = pair.Key;
            FloatingBillboard icon = pair.Value;

            if (target == null || icon == null) continue;

            float dist = Vector3.Distance(transform.position, target.transform.position);
            bool shouldShow = dist <= detectionRadius;

            icon.SetVisible(shouldShow);

            if (shouldShow)
            {
                // Optional: make sure it floats slightly above
                //icon.transform.position = target.transform.position + Vector3.up * 1.5f;
                Debug.Log("Icon Shown");
            }
        }
    }
}
