using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScr : MonoBehaviour
{

    [SerializeField]
    Transform mySpaceShip;

    public float localYangle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SetLocalYAngle (float change)
    {
        localYangle += change;
    }
}
