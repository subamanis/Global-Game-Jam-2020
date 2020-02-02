﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SpaceGameManager : MonoBehaviour
{
    public OrbitsSpace userSelectedObject;
    public CinemachineVirtualCamera virtualCamera;
    List<Transform> bodyParts = new List<Transform>();
    int listCounter = 0;

    private void Start()
    {
        Transform partsParent = FindObjectOfType<Space>().transform.Find("WholeAstronaut");
        foreach (Transform child in partsParent.transform)
        {
            bodyParts.Add(child);
        }
    }

    public OrbitsSpace UserChangesLimb(bool previous)
    {
        Transform part = GetPart(previous);
        virtualCamera.Follow = part;
        return part.gameObject.GetComponent<OrbitsSpace>();
    }

    internal void UserChangesRotation(float delta)
    {
        userSelectedObject.ChangeRotation(delta);
    }

    internal void UserChangesSpeed(float delta)
    {
        userSelectedObject.ChangeSpeed(delta);
    }

    internal Transform GetPart(bool previous)
    {
        if (previous)
        {
            if (listCounter == 0)
            {
                listCounter = bodyParts.Count - 1;
            }
            else
            {
                --listCounter;
            }
        }
        else
        {
            if (listCounter == bodyParts.Count - 1)
            {
                listCounter = 0;
            }
        }
        return bodyParts[listCounter];
    }
}
