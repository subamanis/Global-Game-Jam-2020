using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAstronaut : MonoBehaviour
{
    public PropulsionAccident propulsionAccident;

    public float initialDelay = 2f;
    public float reenableCollisions = 1f;
    public float explosionForce = 10f;
    public float explosionRadius = 20f;

    public OrbitsSpace[] extraItemsToIgnoreCollisions;

    private OrbitsSpace[] itemsToExplode;

    private void Awake()
    {
        itemsToExplode = GetComponentsInChildren<OrbitsSpace>().Union(extraItemsToIgnoreCollisions).ToArray();
    }

    void Start()
    {
        SetIgnoreCollisions(true);
        Invoke(nameof(ExplodeAstronaut), initialDelay);
        propulsionAccident.SetAccidentTime(initialDelay);
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

    internal void CheckIfAllParts()
    {
        if (transform.childCount == 1)
        {
            // We have a whole astronaut
            Debug.Log("You win");
        }
    }
}
