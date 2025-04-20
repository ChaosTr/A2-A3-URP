using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableFruit : MonoBehaviour
{
    public FruitType fruitType;
    public bool isClean = false;
    public Material cleanedMaterial; // Assign per fruit prefab
    private Renderer rend;

    private void Start() => rend = GetComponent<Renderer>();

    public void Clean()
    {
        if (rend != null && cleanedMaterial != null)
            rend.material = cleanedMaterial;

        isClean = true;
    }
}
