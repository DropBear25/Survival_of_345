using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 99f;

   // public AudioSource helicopterClip;


    private bool Helicopter = false;
   [SerializeField] GameObject AudioHelicopter;

    [SerializeField] Text countdownText;

    void Start()
    {
       
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");



        if (currentTime <= 15)
        {
            AudioHelicopter.gameObject.SetActive(true);
            //    helicopter();
            // SoundManager.instance.PlaySoundFX(helicopterClip);
            countdownText.color = Color.red;
          // AudioHelicopter.gameObject.SetActive(false);
        }









        if (currentTime <= 10 )
        {
           
            //    helicopter();
            // SoundManager.instance.PlaySoundFX(helicopterClip);
            countdownText.color = Color.red;
        
          
}

     


        if (currentTime <= 0)
        {
            currentTime = 0;
         //   AudioHelicopter.gameObject.SetActive(false);
            SceneManager.LoadScene(1);

        }

    }
}