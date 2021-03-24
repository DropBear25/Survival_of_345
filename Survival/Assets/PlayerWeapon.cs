using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    private Animator Anim;

    public bool Gun;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gun == true)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Anim.SetBool("Aim", true);
                
                 
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Anim.SetBool("Aim", false);

            }
        }

            

       



    }
}
