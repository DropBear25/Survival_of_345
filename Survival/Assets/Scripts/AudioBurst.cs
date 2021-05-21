using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBurst : MonoBehaviour
{


    public AudioClip burstClip;


   

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.instance.PlaySoundFX(burstClip);
        Destroy(gameObject);
    }


}
