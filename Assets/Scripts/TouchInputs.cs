using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputs : MonoBehaviour
{
    private Vector3 prevValue;

    public float deltaSmoothFactor = 60f;

    public float flingDelta = .2f;

    private enum Modes { None, Rotation, Speed, Fling };

    private Modes userInputMode = Modes.None;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var viewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            prevValue = viewPort;

            if (viewPort.x > .7f)
            {
                userInputMode = Modes.Rotation;
            }
            else if (viewPort.x < 1 - .7f)
            {
                userInputMode = Modes.Speed;
            }
            else
            {
                userInputMode = Modes.Fling;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            var currentValue = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            var delta = (prevValue - currentValue) * Time.deltaTime * deltaSmoothFactor;

            if (userInputMode == Modes.Fling)
            {
                if (delta.x > flingDelta)
                {
                    UserChangesLimb(previous: true);
                }
                else if (delta.x < -flingDelta)
                {
                    UserChangesLimb(previous: false);
                }
            }

            if (userInputMode == Modes.Speed)
            {
                UserChangesSpeed(delta.y);
            }
            else if (userInputMode == Modes.Rotation)
            {
                UserChangesRotation(delta.y);
            }

            prevValue = currentValue;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var viewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            userInputMode = Modes.None;

        }
    }

    private void UserChangesLimb(bool previous)
    {
        Debug.Log("UserChangesLimb " + (previous ?  "previous" : "next"));
    }

    private void UserChangesRotation(float delta)
    {
        Debug.Log("UserChangesRotation " + delta);
    }

    private void UserChangesSpeed(float delta)
    {
        Debug.Log("UserChangesSpeed " + delta);
    }
}
