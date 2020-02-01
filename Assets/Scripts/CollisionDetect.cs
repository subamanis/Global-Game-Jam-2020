using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public ItemOrbit notifyItemOrbit;

    private void OnTriggerEnter(Collider other)
    {
        notifyItemOrbit.TriggerCollision(other.GetComponentInParent<ItemOrbit>());
    }
}
