using System;
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

    public AudioSource backgroundMusic;
    public AudioSource engineFailure;
    public AudioSource explosion;
    int coroutineCounter = 0;

    private void Start()
    {
        Transform partsParent = FindObjectOfType<Space>().transform.Find("WholeAstronaut");
        foreach (Transform child in partsParent.transform)
        {
            bodyParts.Add(child);
        }

        backgroundMusic.Play();

        engineFailure.Play();
        StartCoroutine(PlayEngineFailure());
        StartCoroutine(PlayExplosion());
    }

    IEnumerator PlayEngineFailure()
    {
        yield return new WaitForSeconds(.6f);
        engineFailure.Play();
        if (coroutineCounter++ < 2) StartCoroutine(PlayEngineFailure());
    }

    IEnumerator PlayExplosion()
    {
        yield return new WaitForSeconds(3.25f);
        explosion.Play();
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
            else
            {
                ++listCounter;
            }
        }
        return bodyParts[listCounter];
    }

    internal void LimbWasAttached(OrbitsSpace orbitsSpace)
    {
        bodyParts.Remove(orbitsSpace.transform);
    }
}
