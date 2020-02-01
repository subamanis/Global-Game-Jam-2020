using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitsSpace : MonoBehaviour
{
    public Vector2 moveVector;
    private Space orbitsSpace;
    private ParticleSystem bloodTrailParticles;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        orbitsSpace = GetComponentInParent<Space>();
        bloodTrailParticles = GetComponentInChildren<ParticleSystem>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody.AddRelativeForce(moveVector,ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (rigidBody.position.x > orbitsSpace.bounds.x / 2)
        {
            PauseBloodTrail();
            rigidBody.position = new Vector2(transform.localPosition.x - orbitsSpace.bounds.x, transform.localPosition.y);
            ResumeBloodTrail();
        }
        else if (rigidBody.position.x < -orbitsSpace.bounds.x / 2)
        {
            PauseBloodTrail();
            rigidBody.position = new Vector2(transform.localPosition.x + orbitsSpace.bounds.x, transform.localPosition.y);
            ResumeBloodTrail();
        }

        if (rigidBody.position.y > orbitsSpace.bounds.y / 2)
        {
            PauseBloodTrail();
            rigidBody.position = new Vector2(transform.localPosition.x, transform.localPosition.y - orbitsSpace.bounds.y);
            ResumeBloodTrail();
        }
        else if (rigidBody.position.y < -orbitsSpace.bounds.y / 2)
        {
            PauseBloodTrail();
            rigidBody.position = new Vector2(transform.localPosition.x, transform.localPosition.y + orbitsSpace.bounds.y);
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
