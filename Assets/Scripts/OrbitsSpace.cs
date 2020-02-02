using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitsSpace : MonoBehaviour
{
    private Space orbitsSpace;
    private ParticleSystem bloodTrailParticles;
    private Rigidbody2D rigidBody;

    public float maxVelocity = 10f;
    public float maxAngularVelocity = 90f;

    private void Awake()
    {
        orbitsSpace = GetComponentInParent<Space>();
        bloodTrailParticles = GetComponentInChildren<ParticleSystem>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    internal void ChangeRotation(float delta)
    {
        rigidBody.AddTorque(delta);
    }

    internal void ChangeSpeed(float delta)
    {
        rigidBody.AddForce(-rigidBody.transform.up * delta, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
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

        LimitMaxVelocity();
    }

    private void LimitMaxVelocity()
    {
        float curSpeed = rigidBody.velocity.magnitude;
        if (curSpeed > maxVelocity)
        {
            float reduction = maxVelocity / curSpeed;
            rigidBody.velocity *= reduction;
        }

        rigidBody.angularVelocity = Mathf.Clamp(rigidBody.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPart"))
        {
            if (this.transform.GetSiblingIndex() > collision.transform.GetSiblingIndex())
            {
                this.gameObject.transform.SetParent(collision.transform, worldPositionStays: true);
                DisableThisObjectAsOrbitsSpace();

                GetComponentInParent<DestroyAstronaut>().CheckIfAllParts();
            }
        }
    }

    private void DisableThisObjectAsOrbitsSpace()
    {
        Destroy(this.rigidBody);
        Destroy(this.GetComponent<Collider2D>());
        Destroy(this);
    }
}
