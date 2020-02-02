using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAstronaut : MonoBehaviour
{
    public float initialDelay = 2f;
    public float reenableCollisions = 1f;
    public float explosionForce = 10f;
    public float explosionRadius = 20f;

    private OrbitsSpace[] itemsToExplode;

    private void Awake()
    {
        itemsToExplode = GetComponentsInChildren<OrbitsSpace>();
    }

    void Start()
    {
        SetIgnoreCollisions(true);
        Invoke(nameof(ExplodeAstronaut), initialDelay);
    }

    private void SetIgnoreCollisions(bool ignore)
    {
        foreach (var eachOne in itemsToExplode)
        {
            foreach (var eachOther in itemsToExplode)
            {
                Physics2D.IgnoreCollision(eachOne.GetComponentInChildren<Collider2D>(), eachOther.GetComponentInChildren<Collider2D>(), ignore);
            }
        }
    }

    private void ExplodeAstronaut()
    {
        foreach (var item in itemsToExplode)
        {
            var rigidBodyToExplode = item.GetComponent<Rigidbody2D>();
            Rigidbody2DExtension.AddExplosionForce(rigidBodyToExplode, explosionForce, transform.position, explosionRadius);
        }

        Invoke(nameof(EnableAllCollisions), reenableCollisions);
    }

    private void EnableAllCollisions()
    {
        SetIgnoreCollisions(false);
    }
}
