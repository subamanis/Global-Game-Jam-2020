using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TouchInputs : MonoBehaviour
{
    public SpaceGameManager spaceGameManager;

    public float deltaSmoothFactor = 60f;

    public float flingDelta = .2f;

    private enum Modes { None, Rotation, Speed, Fling };

    private Modes userInputMode = Modes.None;

    private Vector3 prevValue;

    public Image speedIndicator;
    public Image rotationIndicator;
    public Image switchIndicator;

    private void Start()
    {
        ResetIndicators();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var viewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            prevValue = viewPort;

            if (viewPort.x > .75f)
            {
                userInputMode = Modes.Rotation;
            }
            else if (viewPort.x < 1 - .75f)
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
            var delta = (currentValue - prevValue) * Time.deltaTime * deltaSmoothFactor;

            if (userInputMode == Modes.Fling  )
            {
                if (Mathf.Abs(currentValue.x - prevValue.x) > flingDelta)
                {
                    if (delta.x > flingDelta)
                    {
                        UserChangesLimb(previous: true);
                    }
                    else if (delta.x < -flingDelta)
                    {
                        UserChangesLimb(previous: false);
                    }
                    userInputMode = Modes.None; // No more fling for this user action
                }
            }
            else
            {
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
        }
        else if (Input.GetMouseButtonUp(0))
        {
            var viewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            userInputMode = Modes.None;
            ResetIndicators();
        }
    }

    private void ResetIndicators()
    {
        switchIndicator.DOFade(0f, .2f);
        rotationIndicator.DOFade(0f, .2f);
        speedIndicator.DOFade(0f, .2f);
    }

    private void UserChangesLimb(bool previous)
    {
        Debug.Log("UserChangesLimb " + (previous ? "previous" : "next"));
        switchIndicator.DOFade(.5f, .2f);
        switchIndicator.transform.localScale = new Vector3(
            (previous ? 1 : -1) * Mathf.Abs(switchIndicator.transform.localScale.x),
            switchIndicator.transform.localScale.y,
            switchIndicator.transform.localScale.z);

        spaceGameManager.userSelectedObject = spaceGameManager.UserChangesLimb(previous);
    }

    private void UserChangesRotation(float delta)
    {
        Debug.Log("UserChangesRotation " + delta);
        rotationIndicator.DOFade(.5f, .2f);
        if (delta != 0)
        {
            rotationIndicator.transform.localScale = new Vector3(
                -Mathf.Sign(delta) * Mathf.Abs(rotationIndicator.transform.localScale.x),
                rotationIndicator.transform.localScale.y,
                rotationIndicator.transform.localScale.z);
        }
        spaceGameManager.UserChangesRotation(delta);
    }

    private void UserChangesSpeed(float delta)
    {
        Debug.Log("UserChangesSpeed " + delta);
        speedIndicator.DOFade(.5f, .2f);
        if (delta != 0)
        {
            speedIndicator.transform.localScale = new Vector3(
                speedIndicator.transform.localScale.x,
                Mathf.Sign(delta) * Mathf.Abs(speedIndicator.transform.localScale.y),
                speedIndicator.transform.localScale.z);
        }
        spaceGameManager.UserChangesSpeed(delta);
    }
}
