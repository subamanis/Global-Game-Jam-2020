using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGameManager : MonoBehaviour
{
    public OrbitsSpace userSelectedObject;

    internal void UserChangesLimb(bool previous)
    {
    }

    internal void UserChangesRotation(float delta)
    {
        userSelectedObject.ChangeRotation(delta);
    }

    internal void UserChangesSpeed(float delta)
    {
        userSelectedObject.ChangeSpeed(delta);
    }
}
