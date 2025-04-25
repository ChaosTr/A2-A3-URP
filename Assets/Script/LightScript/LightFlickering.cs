using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light flickerLight;
    public bool useRandomFlicker = true;

    [Header("Intensity Settings")]
    public float minIntensity = 0f;
    public float maxIntensity = 1f;

    [Header("Flicker Timing")]
    public float minFlickerTime = 0.05f;
    public float maxFlickerTime = 0.3f;

    [Header("Smooth Flicker")]
    public bool smoothFlicker = false;

    private float timer;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();

        timer = Random.Range(minFlickerTime, maxFlickerTime);
    }

    void Update()
    {
        if (useRandomFlicker)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                float newIntensity = Random.Range(minIntensity, maxIntensity);
                if (smoothFlicker)
                    flickerLight.intensity = Mathf.Lerp(flickerLight.intensity, newIntensity, Time.deltaTime * 10f);
                else
                    flickerLight.intensity = newIntensity;

                timer = Random.Range(minFlickerTime, maxFlickerTime);
            }
        }
    }
}
