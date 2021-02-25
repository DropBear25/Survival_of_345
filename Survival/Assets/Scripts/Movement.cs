using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 80;
    float rot = 0f;
    float gravity = 500;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

     void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("condition", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
            }

        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

}

