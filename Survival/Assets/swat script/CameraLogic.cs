using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    Vector3 m_cameraTarget;
    GameObject m_player;
    PlayerLogic m_playerLogic;

    float m_cameraTargetOffset = 2.0f;
    float m_distanceZ = 2.0f;

    float m_rotationX;
    float m_rotationY;

    //test
    float m_rotationZ;

    const float MIN_X = -20.0f;
    const float MAX_X = 20.0f;

    const float MIN_Z = 2.0f;
    const float MAX_Z = 8.0f;


    Animator m_animator;

    bool m_isAiming = false;

    float m_aimPosX = 0.65f;
    float m_aimPosY = 1.55f;
    float m_aimPosZ = -0.7f;
    float m_aimRotationY;

    //test
    float m_aimRotationZ;


    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_cameraTarget = m_player.transform.position;
        m_cameraTarget.y += m_cameraTargetOffset;   

        if(Input.GetButtonDown("Fire3"))
        {
            m_isAiming = !m_isAiming;

            if(m_isAiming)
            {
                m_aimRotationZ = m_rotationZ;
                 m_aimRotationY = m_rotationY;
                m_player.transform.rotation = Quaternion.Euler(0, m_aimRotationY, 0);
            }
            else
            {
                m_rotationY = m_aimRotationY;
            }
        }

        if(Input.GetButton("Fire2"))
        {
            m_rotationY += Input.GetAxis("Mouse X");
            m_rotationX -= Input.GetAxis("Mouse Y");

            m_rotationX = Mathf.Clamp(m_rotationX, MIN_X, MAX_X);
        }

        m_distanceZ -= Input.GetAxis("Mouse ScrollWheel");
        m_distanceZ = Mathf.Clamp(m_distanceZ, MIN_Z, MAX_Z);
    }

    private void LateUpdate()
    {
        if(!m_isAiming)
        {
            Quaternion cameraRotation = Quaternion.Euler(m_rotationX, m_rotationY, 0);
            Vector3 cameraOffset = new Vector3(0, 0, -m_distanceZ);
            transform.position = m_cameraTarget + cameraRotation * cameraOffset;
            transform.LookAt(m_cameraTarget);
        }
        else
        {
            m_cameraTarget = m_player.transform.position;
            Vector3 cameraOffset = new Vector3(m_aimPosX, m_aimPosY, m_aimPosZ);

            Quaternion cameraRotation = m_player.transform.rotation;

            transform.position = m_cameraTarget + cameraRotation * cameraOffset;
            transform.rotation = Quaternion.Euler(0, m_aimRotationY, 0);
        }
    }

    public Vector3 GetForwardVector()
    {
        Quaternion rotation = Quaternion.Euler(0, m_rotationY, 0);
        return rotation * Vector3.forward;
    }

    public float GetRotationX()
    {
        return m_rotationX;
    }
}
