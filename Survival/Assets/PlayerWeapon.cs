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

    //test
    public AudioSource triggerSound;
    public AudioSource deathSound;
    public AudioSource reloadSound;

    public Transform shotDir;
  //  Rigidbody rb;

    int ammo = 50;
    int maxAmmo = 50;
    int health = 10;
    int maxHealth = 100;


    //test 
    int ammoClip = 0;
    int ammoClipMax = 10;

    void Start()
    {
      //  rb = this.GetComponent<Rigidbody>();
        Crosshair.gameObject.SetActive(false);
        MyPlayer = GetComponent<AudioSource>();

        
        health = maxHealth;
    }

    void ProcessZombieHit()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(shotDir.position, shotDir.forward, out hitInfo, 200))
        {
            GameObject hitZombie = hitInfo.collider.gameObject;
            if(hitZombie.tag == "Zombie")
            {
                GameObject rdPrefab = hitZombie.GetComponent<ZombieAI>().ragdoll;
                GameObject newRD = Instantiate(rdPrefab, hitZombie.transform.position, hitZombie.transform.rotation);
                newRD.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(shotDir.forward * 10000);
                Destroy(hitZombie);
            }
            else
            {
                hitZombie.GetComponent<ZombieAI>().killZombie();
            }

        }
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
                if (ammoClip > 0)
                {
                    //        //Gun Shot Fire
                    
                    MyPlayer.clip = GunShotSound;
                    MyPlayer.Play();
                    ProcessZombieHit();
                    ammoClip--;
                }
                else if (Anim.GetBool("Aim"))
                    triggerSound.Play();

                    Debug.Log("Ammo Left in Clip: " + ammoClip);            
            }


        }//THIS MAY NEED TO BE CHANGED && Anim.GetBool("Aim"))
        if (Input.GetKeyDown(KeyCode.R) && Anim.GetBool("Aim"))
        {
            Anim.SetTrigger("Reload");
            reloadSound.Play();
            int ammoNeeded = ammoClipMax - ammoClip;
            int ammoAvaliable = ammoNeeded < ammo ? ammoNeeded : ammo;
            ammo -= ammoAvaliable;
            ammoClip += ammoAvaliable;
            Debug.Log("Ammo Left: " + ammo);
            Debug.Log("Ammo in Clip" + ammoClip);
        }

    }




    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ammo" && ammo < maxAmmo)
        {
            ammo = Mathf.Clamp(ammo + 10, 0, maxAmmo);
            Debug.Log("Ammo: " + ammo);
            Destroy(col.gameObject);
            //   ammoPickup.Play();


        }
        else if (col.gameObject.tag == "MedBox" && health < maxHealth)
        {

            health = Mathf.Clamp(health + 25, 0, maxHealth);
            Debug.Log("MedBox: " + health);
            Destroy(col.gameObject);
            //healthPickup.Play();

        }
        else if(col.gameObject.tag == "Lava")
        {
            health = Mathf.Clamp(health -50, 0, maxHealth);
            Debug.Log("Health Level: " + health);
            if(health <= 0)
            {
                deathSound.Play();
            }
        }
    }  



}

