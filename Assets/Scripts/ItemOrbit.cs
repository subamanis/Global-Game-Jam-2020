using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ItemOrbit : MonoBehaviour
{
    public float speed = 10f;

    public Vector3 directionMoving;

    private void Update()
    {
        transform.Rotate(directionMoving, speed * Time.deltaTime * 60f);
    }

    internal void TriggerCollision(ItemOrbit itemOrbitCollision)
    {
        if (this.transform.GetSiblingIndex() > itemOrbitCollision.transform.GetSiblingIndex())
        {
            var directionToSwitch = this.directionMoving;
            var speedToSwitch = -this.speed;
            //var rotationToSwitch = this.transform.eulerAngles;

            this.directionMoving = itemOrbitCollision.directionMoving;
            this.speed = itemOrbitCollision.speed;
            //this.transform.eulerAngles = itemOrbitCollision.transform.eulerAngles;

            itemOrbitCollision.directionMoving = directionToSwitch;
            itemOrbitCollision.speed = -speedToSwitch;
            //itemOrbitCollision.transform.eulerAngles = rotationToSwitch;

            Debug.Log("Collision " + gameObject.name + " - " + itemOrbitCollision.name);
        }
        else
        {
            Debug.Log("IGNORE " + gameObject.name + " - " + itemOrbitCollision.name);
        }
    }
}
