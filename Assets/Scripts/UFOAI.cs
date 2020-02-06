using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UFOAI : MonoBehaviour
{
    
    Rigidbody2D thisRigidbody;

    Vector3 playerPosition;
    private GameObject player;
    private GameObject gameObjectLimbToFollow;
    private int limbChildToFollow;

    private float speed = 3;

    public bool chasing = false;

    float chaseInterval;

    Sequence chasingSequence;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("WholeAstronaut").gameObject;
    }

    void Start()
    {

        Sequence squashandstretchAnimation = DOTween.Sequence();
        
        squashandstretchAnimation.Insert(0.0f, transform.DOScaleX(1.1f, 0.3f).SetEase(Ease.InOutSine));
        squashandstretchAnimation.Insert(0.0f, transform.DOScaleY(0.9f, 0.3f).SetEase(Ease.InOutSine));

        squashandstretchAnimation.Insert(0.3f, transform.DOScaleX(0.9f, 0.3f).SetEase(Ease.InOutSine));
        squashandstretchAnimation.Insert(0.3f, transform.DOScaleY(1.1f, 0.3f).SetEase(Ease.InOutSine));
        
        squashandstretchAnimation.SetLink(gameObject)
        .SetLoops(-1, LoopType.Yoyo).Play();

        limbChildToFollow = Random.Range(0, 5);
        gameObjectLimbToFollow = player.transform.GetChild(limbChildToFollow).gameObject;

        speed = Random.Range(0.5f, 2.3f);

    }

    public void ActivateUFO() 
    {
        chaseInterval = Random.Range(4f, 6f);

        chasingSequence = DOTween.Sequence();
        
        chasingSequence.InsertCallback(chaseInterval, () => chasing = true);

        chasingSequence.InsertCallback(chaseInterval*2, () => chasing = false);

        chasingSequence.AppendInterval(chaseInterval);
        
        chasingSequence.SetLoops(-1).SetLink(gameObject).Play();
    }

     public void DeactivateUFO() 
    {
        chasingSequence?.Kill();
        chasing = false;
    }

    void Update() {

        if (!chasing)
            return;

        playerPosition = gameObjectLimbToFollow.transform.position;

        transform.position = Vector2.Lerp(transform.position, playerPosition, speed * Time.deltaTime);
        

    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (collision.gameObject.tag.Equals("HumanPart")) {

            Vector2 direction = new Vector2(collision.contacts[0].point.x, collision.contacts[0].point.y) - new Vector2(transform.position.x, transform.position.y);

            float force = 300;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction*force);

            gameObject.GetComponent<Rigidbody2D>().AddForce(-direction.normalized*force*4);

        //}
    }
     

}
