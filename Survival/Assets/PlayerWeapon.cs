using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public Animator Anim;

    public static bool HaveGun = true;
    [SerializeField] GameObject Crosshair;
    [SerializeField] AudioClip GunShotSound;
    public AudioSource ammoPickup;
    public AudioSource healthPickup;
    private AudioSource MyPlayer;


    int ammo = 0;
    int maxAmmo = 50;    


    void Start()
    {
        Crosshair.gameObject.SetActive(false);
        MyPlayer = GetComponent<AudioSource>();
    }


    
        // Update is called once per frame
        void Update()
    {

        if (HaveGun == true)
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
                //  if (ammoClip > 0)
                //    {
                //        //Gun Shot Fire
                       MyPlayer.clip = GunShotSound;
                        MyPlayer.Play();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Anim.SetTrigger("Reload");
        }

    }




    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ammo"))
        {
            ammo += 10;
            Debug.Log("Ammo" + ammo);
            Destroy(col.gameObject);
            //   ammoPickup.Play();


        }
        else if (col.gameObject.tag == "MedBox")
        {
            Debug.Log("MedBox");
            Destroy(col.gameObject);
            //healthPickup.Play();

        }
    }



}

