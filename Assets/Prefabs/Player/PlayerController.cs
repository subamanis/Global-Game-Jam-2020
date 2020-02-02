using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Limbs")]

    [SerializeField] GameObject limbHead;
    [SerializeField] GameObject limbBody;
    [SerializeField] GameObject limbArmFront;
    [SerializeField] GameObject limbArmBehind;
    [SerializeField] GameObject limbLegFront;
    [SerializeField] GameObject limbLegBehind;

    [Header("Attachments")]
    [SerializeField] GameObject attachmentRocket;

    [Header("Visual FX")]
    [SerializeField] GameObject fxExhaust;

    void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        
        // TODO move to cursor
        Vector3 mousePosition = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 10 * Time.deltaTime);

        // flicker exhaust
        fxExhaust.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.PingPong(Time.time*200, 1f));

    }

}
