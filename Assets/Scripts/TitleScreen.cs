using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    
    public string sceneToLoad = "GameJamScene";

    public AudioClip soundStart;
    private AudioSource soundStartAudioSource;

    public Image startButtonImage;

    void Start()
    {
        soundStartAudioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonTap()
    {
        startButtonImage.GetComponent<Button>().interactable = false;
        
        soundStartAudioSource.PlayOneShot(soundStart);
        
        startButtonImage.color = new Color(1, 1, 1, 1);
        startButtonImage.GetComponent<StartButtonBlink>().blinkMultiplier = 8;

        Sequence startLevelSequence = DOTween.Sequence();
        
        startLevelSequence.AppendInterval(1);
        
        startLevelSequence.OnComplete(() => {
        
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        
        })
        .Play();
    }


}
