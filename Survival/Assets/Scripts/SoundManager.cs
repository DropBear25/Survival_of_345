using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public AudioSource soundFX;


 //   public AudioClip[] horrorMusic;

    

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       
    }




    void Update()
    {
       
    }

    public void PlaySoundFX(AudioClip clip)
    {
        soundFX.clip = clip;
        soundFX.volume = UnityEngine.Random.Range(.5f, .7f);
        //soundFX.pitch = Random.Range(.8f, 1);
        soundFX.Play();
    }

    //internal void PlaySoundFX(AudioSource helicopterClip)
    //{
    //    throw new NotImplementedException();
    //}

    public void PlayZombieFX(AudioClip z_clip)
    {
        soundFX.clip = z_clip;
        soundFX.volume = UnityEngine.Random.Range(.2f, .5f);
       // soundFX.pitch = UnityEngine.Random.Range(.3f, 1);
        soundFX.Play();
    }

}
