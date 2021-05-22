using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRadar : MonoBehaviour
{


    [SerializeField] GameObject Radar;
   public AudioSource Audio2;
    public bool alreadyPlayed = false;

    private void Start()
    {
        Audio2 = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
       Radar.gameObject.SetActive(false);
        alreadyPlayed = true;
      Audio2.Play();  
    }



}
