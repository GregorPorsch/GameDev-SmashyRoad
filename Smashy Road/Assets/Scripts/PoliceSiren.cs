using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSiren : MonoBehaviour
{
    public Light[] sirens;
    public float lightSteps = .1f;
    public float maxIntensity = 7;

    bool redIncrease;       // true: blue decrease, red increase

    // Update is called once per frame
    void Update()
    {
        if (redIncrease)
        {
            StartCoroutine(decreaseIntensity(sirens[0]));
            StartCoroutine(decreaseIntensity(sirens[1]));
            StartCoroutine(increaseIntensity(sirens[2]));
            StartCoroutine(increaseIntensity(sirens[3]));
            redIncrease = false;
        }
        else
        {
            StartCoroutine(decreaseIntensity(sirens[2]));
            StartCoroutine(decreaseIntensity(sirens[3]));
            StartCoroutine(increaseIntensity(sirens[0]));
            StartCoroutine(increaseIntensity(sirens[1]));
            redIncrease = true;
        }
    }

    IEnumerator decreaseIntensity(Light siren)
    {
        while (siren.intensity > 0)
        {
            siren.intensity -= lightSteps;
        }
        yield return null;
    }

    IEnumerator increaseIntensity(Light siren)
    {
        while (siren.intensity < maxIntensity)
        {
            siren.intensity += lightSteps;
        }
        yield return null;
    }
}
