using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{

    //  public GameObject zombiePrefab;
    //


    private void Start()
    {
     //   GetComponent<RadarBlips>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        KillZombies();
      
        Debug.Log("Destroyed all zombies");

    }


    void KillZombies()
    {
        GameObject.FindGameObjectWithTag("Zombie");
        Destroy(GameObject.FindWithTag("Zombie"));
      
     
            RadarBlips.enable = false;
        
    
    }
}