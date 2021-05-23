using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerWeapon : MonoBehaviour
{

    public Animator Anim;
    public Slider healthbar;
    public Text ammoReserves;
    public Text ammoClipAmount;

    public GameObject bloodPrefab;
    public GameObject uiBloodPrefab;
    public GameObject uiCanvas;


    public static bool HaveGun = true;
    [SerializeField] GameObject Crosshair;
    // [SerializeField] AudioClip GunShotSound;
  //  public AudioSource ammoPickup;
 //   public AudioSource healthPickup;
    // private AudioSource MyPlayer;

    //test
    //  public AudioSource triggerSound;
    //  public AudioSource deathSound;
    // public AudioSource reloadSound;


    //test
    public AudioClip[] shootClips;
    public AudioClip shootClip;
    public AudioClip reloadClip;
    public AudioClip emptyTriggerClip;
    public AudioClip deathClip;

    public AudioClip ammoPickupClip;
    public AudioClip healthPickupClip;
    public AudioClip hitClip;
    public AudioClip zombiehitClip;

    public Transform shotDir;

    //  Rigidbody rb;

    int ammo = 50;
    int maxAmmo = 50;
    int health = 100;
    int maxHealth = 100;



    int ammoClip = 0;
    int ammoClipMax = 10;


    float canvasWidth;
    float canvasHeight;


    //test 
    //  public static bool canShoot = true;


    public void TakeHit(float amount)
    {
       
        SoundManager.instance.PlaySoundFX(hitClip);
        health = (int)Mathf.Clamp(health - amount, 0, maxHealth);
        healthbar.value = health;


        GameObject bloodSplatter = Instantiate(uiBloodPrefab);
        bloodSplatter.transform.SetParent(uiCanvas.transform);
        bloodSplatter.transform.position = new Vector3(Random.Range(0, canvasWidth), Random.Range(0, canvasHeight), 0);


        Destroy(bloodSplatter, 2.2f);


        //Debug.Log("Health " + health);

        if (health <= 0)
        {
            StartCoroutine(PlayerDeath());
            //PUT IN AN ENUMERATOR AND GAME OVER SCENE HERE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
          
        }
    }

    IEnumerator PlayerDeath()
    {
        SoundManager.instance.PlaySoundFX(deathClip);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(3);
        //scene manager
    }
    void Start()
    {
    
        Crosshair.gameObject.SetActive(false);
      

        values();
        
     
    }

    private void values()
    {
          health = maxHealth;
        healthbar.value = health;
        ammoReserves.text = ammo + "";
        ammoClipAmount.text = ammoClip + "";
    }

    void ProcessZombieHit()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(shotDir.position, shotDir.forward, out hitInfo, 200))
        {
            GameObject hitZombie = hitInfo.collider.gameObject;
            if (hitZombie.tag == "Zombie")
            {//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX different volume hit
                GameObject blood = Instantiate(bloodPrefab, hitInfo.point, Quaternion.identity);
                blood.transform.LookAt(this.transform.position);
                SoundManager.instance.PlayZombieFX(zombiehitClip);
                Destroy(blood, 0.5f);

                hitZombie.GetComponent<ZombieAI>().shotsTaken++;
                if (hitZombie.GetComponent<ZombieAI>().shotsTaken ==
                 hitZombie.GetComponent<ZombieAI>().shotsRequired)

                    if (Random.Range(0, 10) < 5)
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && Anim.GetBool("Aim"))  
            {
                if (ammoClip > 0)
                {
                    //        //Gun Shot Fire

                    // MyPlayer.clip = GunShotSound;
                    // MyPlayer.Play();

                    ammoClip--;
                    ammoClipAmount.text = ammoClip + "";
                    SoundManager.instance.PlaySoundFX(shootClip);
                    ProcessZombieHit();
                   
                    //test 
                  //  canShoot = false;
                }
                else if (Anim.GetBool("Aim"))
                    SoundManager.instance.PlaySoundFX(emptyTriggerClip);
             //   triggerSound.Play();
                //Empty gun xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
                          
            }


        }
        if (Input.GetKeyDown(KeyCode.R) && Anim.GetBool("Aim"))
        {
            Anim.SetTrigger("Reload");
            SoundManager.instance.PlaySoundFX(reloadClip);
          //  reloadSound.Play(); //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            int ammoNeeded = ammoClipMax - ammoClip;
            int ammoAvaliable = ammoNeeded < ammo ? ammoNeeded : ammo;
            ammo -= ammoAvaliable;
            ammoClip += ammoAvaliable;
            ammoReserves.text = ammo + "";
            ammoClipAmount.text = ammoClip + "";

            canvasWidth = uiCanvas.GetComponent<RectTransform>().rect.width;
            canvasHeight = uiCanvas.GetComponent<RectTransform>().rect.height;


          //  Debug.Log("Ammo Left: " + ammo);
          //  Debug.Log("Ammo in Clip" + ammoClip);
        }

    }




    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ammo" && ammo < maxAmmo)
        {
            ammo = Mathf.Clamp(ammo + 10, 0, maxAmmo);
          //  Debug.Log("Ammo: " + ammo);
            Destroy(col.gameObject);
            ammoReserves.text = ammo + "";
            SoundManager.instance.PlaySoundFX(ammoPickupClip);
            //   ammoPickup.Play();


        }
        else if (col.gameObject.tag == "MedBox" && health < maxHealth)
        {

            health = Mathf.Clamp(health + 25, 0, maxHealth);
            healthbar.value = health;
           // Debug.Log("MedBox: " + health);
            Destroy(col.gameObject);
            SoundManager.instance.PlaySoundFX(healthPickupClip);
            //healthPickup.Play();

        }
     
    }  



}

