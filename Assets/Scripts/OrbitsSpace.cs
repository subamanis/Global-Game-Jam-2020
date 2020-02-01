using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitsSpace : MonoBehaviour
{
    public Vector2 moveVector;
    private Space orbitsSpace;
    private ParticleSystem bloodTrailParticles;

    private void Awake()
    {
        orbitsSpace = GetComponentInParent<Space>();
        bloodTrailParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        transform.Translate(moveVector * Time.deltaTime * 60f, UnityEngine.Space.Self);
        if (transform.localPosition.x > orbitsSpace.bounds.x / 2)
        {
            PauseBloodTrail();
            transform.localPosition = new Vector2(transform.localPosition.x - orbitsSpace.bounds.x, transform.localPosition.y);
            ResumeBloodTrail();
        }
        else if (transform.localPosition.x < -orbitsSpace.bounds.x / 2)
        {
            PauseBloodTrail();
            transform.localPosition = new Vector2(transform.localPosition.x + orbitsSpace.bounds.x, transform.localPosition.y);
            ResumeBloodTrail();
        }

        if (transform.localPosition.y > orbitsSpace.bounds.y / 2)
        {
            PauseBloodTrail();
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - orbitsSpace.bounds.y);
            ResumeBloodTrail();
        }
        else if (transform.localPosition.x < -orbitsSpace.bounds.x / 2)
        {
            PauseBloodTrail();
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + orbitsSpace.bounds.y);
            ResumeBloodTrail();
        }
    }

    private void ResumeBloodTrail()
    {
        ParticleSystem.EmissionModule emission = bloodTrailParticles.emission;
        //emission.enabled = true;
        bloodTrailParticles.Play();
    }

    private void PauseBloodTrail()
    {
        ParticleSystem.EmissionModule emission = bloodTrailParticles.emission;
        bloodTrailParticles.Pause();
        //emission.enabled = false;
    }
}
