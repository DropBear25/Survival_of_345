using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField]
    Transform m_bulletSpawnPos;

    [SerializeField]
    GameObject m_impactPos;
    MeshRenderer m_impactMeshRenderer;

    LineRenderer m_lineRenderer;

    float m_lineRendererLength = 10.0f;

    const int MAX_BULLETS = 30;
    int m_bulletCount = MAX_BULLETS;

    const float MAX_COOLDOWN = 0.1f;
    float m_shotCooldown = MAX_COOLDOWN;

    AudioSource m_audioSource;

    [SerializeField]
    AudioClip m_shotSound;

    [SerializeField]
    AudioClip m_reloadSound;

    [SerializeField]
    AudioClip m_emptySound;

    Animator m_animator;

    bool m_isReloading = false;

    [SerializeField]
    GameObject m_gunShotCrack;

    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();

        if(m_impactPos)
        {
            m_impactMeshRenderer = m_impactPos.GetComponent<MeshRenderer>();
            m_impactMeshRenderer.enabled = false;
        }

        m_audioSource = GetComponent<AudioSource>();

        m_animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineRenderer();

        if(m_shotCooldown > 0)
        {
            m_shotCooldown -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Fire1") && m_shotCooldown <= 0.0f && !m_isReloading)
        {
            if(m_bulletCount > 0)
            {
                Shoot();
            }
            else
            {
                // Empty
                PlaySound(m_emptySound);
            }

            m_shotCooldown = MAX_COOLDOWN;
        }

        if(Input.GetKeyDown(KeyCode.R) && !m_isReloading)
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (m_animator)
        {
            m_animator.SetTrigger("Shoot");
        }

        PlaySound(m_shotSound);
        --m_bulletCount;

        Ray ray = new Ray(m_bulletSpawnPos.position, m_bulletSpawnPos.transform.forward);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 100.0f))
        {
            Vector3 impactPos = rayHit.point;
            impactPos += rayHit.normal * 0.001f;

            GameObject impactObj = Instantiate(m_gunShotCrack, impactPos, Quaternion.identity, null);
            impactObj.transform.up = rayHit.normal;
        }
    }

    void Reload()
    {
        if (m_animator)
        {
            m_animator.SetTrigger("Reload");
        }

        m_isReloading = true;
        m_bulletCount = MAX_BULLETS;
        PlaySound(m_reloadSound);
    }

    public void SetReloadingState(bool isReloading)
    {
        m_isReloading = isReloading;
    }

    void PlaySound(AudioClip sound)
    {
        if(m_audioSource && sound)
        {
            m_audioSource.PlayOneShot(sound);
        }
    }

    void UpdateLineRenderer()
    {
        if(m_lineRenderer)
        {
            m_lineRenderer.SetPosition(0, m_bulletSpawnPos.position);

            Ray ray = new Ray(m_bulletSpawnPos.position, m_bulletSpawnPos.transform.forward);
            RaycastHit rayHit;

            if(Physics.Raycast(ray, out rayHit, m_lineRendererLength))
            {
                m_lineRenderer.SetPosition(1, rayHit.point);
                m_impactPos.transform.position = rayHit.point;
                m_impactMeshRenderer.enabled = true;
            }
            else
            {
                m_lineRenderer.SetPosition(1, m_bulletSpawnPos.position + m_bulletSpawnPos.transform.forward * m_lineRendererLength);
                m_impactMeshRenderer.enabled = false;
            }
        }
    }

    public bool IsReloading()
    {
        return m_isReloading;
    }
}
