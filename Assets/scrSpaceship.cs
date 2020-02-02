using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSpaceship : MonoBehaviour
{

    [SerializeField]
    Transform planet;

    [SerializeField]
    Transform myCapsule;

    [SerializeField]
    CapsuleScr myCapsuleScr;


    [SerializeField]
    float linearMagni = 100;

    [SerializeField]
    float turnMagni = 100;

    [SerializeField]
    float heightForceMagni = 100f;

    [SerializeField]
    Rigidbody rb;
    private float h;
    private float v;

    [SerializeField]
    AnimationCurve heightRudderCurve;

    [SerializeField]
    float angleToRotateXLocal;

    [SerializeField]
    bool useControls = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");


    }

    void FixedUpdate()
    {
        LookPlanet();

        if (useControls)
        {
            if (h != 0f)


            {
                //AddForce(transform.right * linearMagni);

                //myCapsule.localRotation *= Quaternion.Euler(0f, h * turnMagni*Time.deltaTime, 0f);

                myCapsuleScr.SetLocalYAngle(h * Time.deltaTime * turnMagni);
            }

            if (v != 0f)
            {
                AddForce(myCapsule.transform.forward * linearMagni);
            }
        }
    }

    private void AddForce(Vector3 _force)
    {
        rb.AddForce(_force, ForceMode.Force);
    }


    private void LookPlanet()
    {

        Vector3 vectorToPlanet = transform.position - planet.position;

        //transform.forward = Vector3.Cross(transform.right, vectorToPlanet.normalized);

        transform.up = vectorToPlanet;
        float offSetHeight = vectorToPlanet.magnitude - 10f;

        float multiFromCurve = heightRudderCurve.Evaluate(Mathf.Abs(offSetHeight) / 2.5f);

        transform.position = transform.position - transform.up * offSetHeight;

        myCapsule.position = transform.position;
        myCapsule.up = transform.up;

        Vector3 beforeYfix = transform.rotation.eulerAngles;
        beforeYfix.y = myCapsuleScr.localYangle;
        transform.rotation = Quaternion.Euler(beforeYfix);



        /*
        if (offSetHeight < 0f)
        {
            rb.AddForce(transform.up* heightForceMagni* multiFromCurve);
        }

        if (offSetHeight > 0f)
        {
            rb.AddForce(-1f*transform.up* heightForceMagni * multiFromCurve);
        }
        */

        //   transform.up = myCapsule.up;
        /*
        angleToRotateXLocal = Vector3.Angle(transform.up, vectorToPlanet);
        transform.localRotation = transform.localRotation * Quaternion.Euler(-angleToRotateXLocal,0f,0f);
    */
    }
}
