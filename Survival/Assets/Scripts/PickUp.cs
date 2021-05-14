using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    int ammo = 0;



   
    
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ammo"))
        {
            ammo += 10;
            Debug.Log("Ammo" + ammo);
            Destroy(collider.gameObject);
            //   ammoPickup.Play();


        }
        else if (collider.gameObject.tag == "MedBox")
        {
            Debug.Log("MedBox");
            Destroy(collider.gameObject);
            //healthPickup.Play();

        }
    }
}
