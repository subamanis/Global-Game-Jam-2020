using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeForce : MonoBehaviour
{
    public float moveSpeed = 10f;

    Rigidbody m_Rigidbody;
    Vector3 m_NewForce;

    public enum Directions { None, Forward, Right, Up };

    public Directions directionToUse;

    // Start is called before the first frame update
    void Start()
    {
        //You get the Rigidbody component you attach to the GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.DrawRay(m_Rigidbody.position, m_Rigidbody.transform.right * 5, Color.white);
        //Debug.DrawRay(m_Rigidbody.position, m_Rigidbody.transform.forward * 5, Color.blue);
        Vector3 dir = Vector3.zero;
        if (directionToUse == Directions.Forward)
            dir = m_Rigidbody.transform.forward;
        else if (directionToUse == Directions.Right)
            dir = m_Rigidbody.transform.right;
        else if (directionToUse == Directions.Up)
            dir = m_Rigidbody.transform.up;

        //Debug.DrawRay(m_Rigidbody.position, m_Rigidbody.velocity, Color.white);
        m_NewForce = dir * moveSpeed;
        //Debug.DrawRay(m_Rigidbody.position, m_NewForce, Color.magenta);
        m_Rigidbody.AddForce(m_NewForce);

        RemoveLocalZVelocity();

        //var velocity = m_Rigidbody.velocity;
        //var escapeVelocity = Vector3.Project(velocity, m_Rigidbody.transform.up);

        //float v = Vector3.Dot(escapeVelocity, m_Rigidbody.transform.up);
        //Debug.Log(v);
        //if (Mathf.Sign(v) == 1f)
        //{
        //    m_Rigidbody.AddForce(-escapeVelocity);
        //    //velocity -= escapeVelocity;
        //    Debug.DrawRay(m_Rigidbody.position, escapeVelocity, Color.yellow);
        //}
        //else
        //{
        //    Debug.DrawRay(m_Rigidbody.position, escapeVelocity, Color.red);
        //}

        //m_Rigidbody.velocity = velocity;
    }

    void RemoveLocalZVelocity()
    {
        // C# / UnityScript
        var yVel = m_Rigidbody.velocity.y;
        var localV = transform.InverseTransformDirection(m_Rigidbody.velocity);
        localV.y = 0.0f;
        var worldV = transform.TransformDirection(localV);
        worldV.y = yVel;
        m_Rigidbody.velocity = worldV;
    }
}
