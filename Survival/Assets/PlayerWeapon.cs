using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public Animator Anim;

    public static bool HaveGun = true;
    [SerializeField] GameObject Crosshair;
    [SerializeField] AudioClip GunShotSound;
    private AudioSource MyPlayer;
    


    void Start()
    {
        Crosshair.gameObject.SetActive(false);
        MyPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(HaveGun == true)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Anim.SetBool("Aim", true);
                Crosshair.gameObject.SetActive(true);


            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Anim.SetBool("Aim", false);
                Crosshair.gameObject.SetActive(false);

            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MyPlayer.clip = GunShotSound;
                MyPlayer.Play();
            }
        }

            

       



    }
}
