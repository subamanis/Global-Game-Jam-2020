using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    
    public string sceneToLoad = "GameJamScene";

    public AudioClip soundStart;
    private AudioSource soundStartAudioSource;

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
        soundStartAudioSource.PlayOneShot(soundStart);
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
    }


}
