using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    float m_horizontalInput;
    float m_verticalInput;

    float m_movementSpeed = 5.0f;

    bool m_jump = false;
    float m_jumpHeight = 0.25f;
    float m_gravity = 0.981f;

    Vector3 m_heightMovement;
    Vector3 m_verticalMovement;
    Vector3 m_horizontalMovement;

    CharacterController m_characterController;

    Animator m_animator;

    [SerializeField]
    List<AudioClip> m_footstepStoneSounds = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> m_footstepEarthSounds = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> m_footstepGrassSounds = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> m_footstepPuddleSounds = new List<AudioClip>();

    AudioSource m_audioSource;

    [SerializeField]
    Transform m_leftFoot;

    [SerializeField]
    Transform m_rightFoot;

    GameObject m_camera;
    CameraLogic m_cameraLogic;

    bool m_isCrouching = false;

    [SerializeField]
    Transform m_leftHandTransform;

    WeaponLogic m_weaponLogic;

    float m_rayCastHeightOffset = 0.5f;
    float m_rayCastPositioningOffset = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_camera = Camera.main.gameObject;
        if(m_camera)
        {
            m_cameraLogic = m_camera.GetComponent<CameraLogic>();
        }

        m_weaponLogic = GetComponentInChildren<WeaponLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && m_characterController.isGrounded)
        {
            m_isCrouching = !m_isCrouching;

            if (m_animator)
            {
                m_animator.SetBool("IsCrouching", m_isCrouching);
            }
        }

        if(m_isCrouching)
        {
            m_horizontalInput = 0;
            m_verticalInput = 0;
            return;
        }

        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        if(Input.GetButtonDown("Jump") && m_characterController.isGrounded)
        {
            m_jump = true;
        }

        if(m_animator)
        {
            m_animator.SetFloat("HorizontalInput", m_horizontalInput);
            m_animator.SetFloat("VerticalInput", m_verticalInput);
        }
    }

    private void FixedUpdate()
    {
        if(m_jump)
        {
            m_heightMovement.y = m_jumpHeight;
            m_jump = false;
        }

        if(m_cameraLogic && (Mathf.Abs(m_horizontalInput) > 0.1f || Mathf.Abs(m_verticalInput) > 0.1f))
        {
            transform.forward = m_cameraLogic.GetForwardVector();
        }

        m_heightMovement.y -= m_gravity * Time.deltaTime;

        m_verticalMovement = transform.forward * m_verticalInput * m_movementSpeed * Time.deltaTime;
        m_horizontalMovement = transform.right * m_horizontalInput * m_movementSpeed * Time.deltaTime;

        m_characterController.Move(m_horizontalMovement + m_verticalMovement + m_heightMovement);

        if(m_characterController.isGrounded)
        {
            m_heightMovement.y = 0.0f;
        }
    }

    public void PlayFootstepSound(int footIndex)
    {
        // 0 = left, 1 = right
        if(footIndex == 0)
        {
            RayCastTerrain(m_leftFoot.position);
        }
        else if(footIndex == 1)
        {
            RayCastTerrain(m_rightFoot.position);
        }
    }

    void RayCastTerrain(Vector3 position)
    {
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, layerMask))
        {
            string hitTag = hit.collider.gameObject.tag;
            if(hitTag == "Earth")
            {
                PlayRandomSound(m_footstepEarthSounds);
            }
            else if(hitTag == "Stone")
            {
                PlayRandomSound(m_footstepStoneSounds);
            }
            else if(hitTag == "Grass")
            {
                PlayRandomSound(m_footstepGrassSounds);
            }
            else if(hitTag == "Puddle")
            {
                PlayRandomSound(m_footstepPuddleSounds);
            }
        }
    }

    void PlayRandomSound(List<AudioClip> audioClips)
    {
        if (audioClips.Count > 0 && m_audioSource)
        {
            int randomNum = Random.Range(0, audioClips.Count - 1);
            m_audioSource.PlayOneShot(audioClips[randomNum]);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(m_animator && m_cameraLogic)
        {
            m_animator.SetBoneLocalRotation(HumanBodyBones.Neck, Quaternion.Euler(m_cameraLogic.GetRotationX(), 0, 0));
            m_animator.SetBoneLocalRotation(HumanBodyBones.RightShoulder, Quaternion.Euler(m_cameraLogic.GetRotationX(), 0, 0));
            m_animator.SetBoneLocalRotation(HumanBodyBones.LeftShoulder, Quaternion.Euler(m_cameraLogic.GetRotationX(), 0, 0));
        }

        if(m_leftHandTransform && m_animator)
        {
            if(!m_weaponLogic.IsReloading())
            {
                m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                m_animator.SetIKPosition(AvatarIKGoal.LeftHand, m_leftHandTransform.position);
                m_animator.SetIKRotation(AvatarIKGoal.LeftHand, m_leftHandTransform.rotation);
            }
            else
            {
                m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }

        if(m_leftFoot)
        {
            UpdateFootIK(AvatarIKGoal.LeftFoot, m_leftFoot);
        }

        if (m_rightFoot)
        {
            UpdateFootIK(AvatarIKGoal.RightFoot, m_rightFoot);
        }
    }

    void UpdateFootIK(AvatarIKGoal avatarIKGoal, Transform footTransform)
    {
        Vector3 rayStartPos = footTransform.position;
        rayStartPos.y += m_rayCastHeightOffset;

        Ray ray = new Ray(rayStartPos, Vector3.down);
        RaycastHit rayHit;
        LayerMask obstacleLayerMask = LayerMask.GetMask("Obstacle");

        if (Physics.Raycast(ray, out rayHit, 1.0f, obstacleLayerMask))
        {
            Vector3 targetPos = rayHit.point;
            targetPos.y += m_rayCastPositioningOffset;

            m_animator.SetIKPositionWeight(avatarIKGoal, 1);
            m_animator.SetIKRotationWeight(avatarIKGoal, 1);
            m_animator.SetIKPosition(avatarIKGoal, targetPos);
            m_animator.SetIKRotation(avatarIKGoal, footTransform.rotation);
        }
    }
}
