using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropulsionAccident : MonoBehaviour
{
    public GameObject fireObject;

    public AnimationCurve fireAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private float currentTime;
    private float accidentDuration;
    bool accidentHappening = false;

    internal void SetAccidentTime(float duration)
    {
        currentTime = 0;
        accidentHappening = true;
        accidentDuration = duration;
        Invoke(nameof(AccidentDone), duration);
    }

    private void AccidentDone()
    {
        accidentHappening = false;
        fireObject.SetActive(false);
    }

    private void Update()
    {
        if (accidentHappening)
        {
            currentTime += Time.deltaTime;
            var currentValue = fireAnimationCurve.Evaluate(currentTime / accidentDuration);

            fireObject.SetActive(currentValue > .5f);
        }
    }
}
