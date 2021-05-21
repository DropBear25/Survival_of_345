using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRadar : MonoBehaviour
{


    [SerializeField] GameObject Radar;
   public AudioClip whitenoiseClip;



    private void OnTriggerEnter(Collider other)
    {
        Radar.gameObject.SetActive(false);
        SoundManager.instance.PlaySoundFX(whitenoiseClip);

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //Audio "Radar fuzzy it looks like its gone off, keep following the trail you should see a house go to it!"
    }
}
