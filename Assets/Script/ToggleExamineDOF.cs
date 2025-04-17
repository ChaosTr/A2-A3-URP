using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ToggleExamineDOF : MonoBehaviour
{
    public Volume postProcessVolume; // Drag your Global Volume here in the Inspector
    private DepthOfField dof;
    private bool examining = false;

    void Start()
    {
        // Try to get the Depth of Field override from the Volume
        if (postProcessVolume != null && postProcessVolume.profile.TryGet(out dof))
        {
            dof.active = false; // Ensure it's off at start
        }
        else
        {
            Debug.LogWarning("Depth of Field not found on Volume Profile!");
        }
    }

    void Update()
    {
        // Press I to turn ON examine mode and enable DOF
        if (Input.GetKeyDown(KeyCode.I))
        {
            examining = true;
            if (dof != null)
            {
                dof.active = true;
                Debug.Log("Depth of Field enabled");
            }
        }

        // Press ESC to exit examine mode and disable DOF
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            examining = false;
            if (dof != null)
            {
                dof.active = false;
                Debug.Log("Depth of Field disabled");
            }
        }
    }
}
