using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DestroyAstronaut : MonoBehaviour
{
    public PropulsionAccident propulsionAccident;
    public GameObject winConditionText;

    public float initialDelay = 3.3f;
    public float reenableCollisions = 1f;
    public float explosionForce = 10f;
    public float explosionRadius = 20f;

    public TouchInputs touchInputs;

    public AudioSource soundJingleComplete;
    public AudioSource soundBackgroundMusic;

    public AudioSource playerHitScream;

    public ParticleSystem particlesExplosion;

    public OrbitsSpace[] extraItemsToIgnoreCollisions;

    private OrbitsSpace[] itemsToExplode;

    private void Awake()
    {
        winConditionText.SetActive(false);
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
            rigidBodyToExplode.AddTorque(UnityEngine.Random.Range(-500, 500), ForceMode2D.Force);
        }

        ShowExplosionFX();
        ActivateAllEnemies();

        Invoke(nameof(EnableAllCollisions), reenableCollisions);
        AllowPlayerControl();
    }

    private void AllowPlayerControl()
    {
        touchInputs.controlsEnabled = true;
        //CheckIfAllParts(true); // for testing animations
    }

    private void ShowExplosionFX()
    {
        particlesExplosion.transform.position = gameObject.transform.position;
        particlesExplosion.Play();
        playerHitScream.Play();
    }

    private void ActivateAllEnemies()
    {
        UFOAI[] ufos = FindObjectsOfType<UFOAI>();
        foreach (UFOAI ufo in ufos) {
            ufo.chasing = true;
        }
    }

    private void DeactivateAllEnemies()
    {
        UFOAI[] ufos = FindObjectsOfType<UFOAI>();
        foreach (UFOAI ufo in ufos) {
            ufo.chasing = false;
        }
    }

    private void EnableAllCollisions()
    {
        SetIgnoreCollisions(false);
    }

    internal void CheckIfAllParts(bool test = false)
    {
        if (transform.childCount == 1 || test)
        {
            // We have a whole astronaut
            Debug.Log("You win");

            soundBackgroundMusic.Stop();
            soundJingleComplete.Play();

            DeactivateAllEnemies();

            winConditionText.SetActive(true);
            winConditionText.transform.GetChild(0).transform.DOScale(0.5f, 0.9f).From().SetEase(Ease.OutElastic).Play();

            Sequence restartGame = DOTween.Sequence();
            
            restartGame.AppendInterval(10.0f);

            restartGame.InsertCallback(9, () => {

                winConditionText.transform.GetChild(0).transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).Play();

            });
            
            restartGame.OnComplete(() => {
            
                Debug.Log("Restarting...");
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            
            })
            .Play();

        }
    }
}
