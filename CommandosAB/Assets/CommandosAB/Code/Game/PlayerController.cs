﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
    //public float m_Speed;
    CharacterController m_CharacterController;
    Animator m_Animator;

    public int m_Score = 0;
    public int m_Grenades = 2;
    public int m_Lifes = 3;
    public GameObject m_AmmoPrefab;
    public float m_ShootRate = 1.0f;
    float m_CurrentShootTime = 0.0f;
    public AmmoContainer m_AmmoContainer;
    public Transform m_OutputAmmo;
    public GameObject m_GrenadePrefab;
    GameObject m_CurrentGrenade;
    public Transform m_OutputGrenadeTransform;
    int l_ThrowGrenadeIdAnimation = Animator.StringToHash("ThrowGrenade");
    int l_DeadIdAnimation = Animator.StringToHash("Dead");
    public float m_GrenadeSpeedXZ;
    public float m_GrenadeSpeedY;
    bool m_CanShootGrenade;
    Vector3 m_StartPosition;
    Quaternion m_StartRotation;


    // Use this for initialization
    void Start () 
	{
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        m_CurrentShootTime = 0.0f;
        m_CanShootGrenade = true;
        m_CurrentGrenade = null;
        m_StartRotation = transform.rotation;
        m_StartPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () 
	{
        AnimatorStateInfo l_AnimatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (m_Animator.GetBool ("Dead"))
        {
            if (l_AnimatorStateInfo.shortNameHash == l_DeadIdAnimation && l_AnimatorStateInfo.normalizedTime >= 1.0f)
            {
                RestartGame();
            }
            return;
        }
        Vector3 l_Direction = Vector3.zero;
        float l_DesiredAngle = transform.rotation.eulerAngles.y;
        bool l_NewAngle = false;
        float l_HorizontalAxis = Input.GetAxis("Horizontal");
        float l_VerticalAxis = Input.GetAxis("Vertical");
        bool l_FirePressed = Input.GetButton("Fire1");
        bool l_ThrowGrenadePressed = Input.GetButton("Fire2");

        m_Animator.SetBool("Shoot", l_FirePressed);
        
        m_CurrentShootTime -= Time.deltaTime;

        if (l_FirePressed && CanShoot())
        {
            Shoot(transform.forward);
        }
        /*if (l_FirePressed)
        {
            Debug.Log("Shooting");
        }*/

        //if (Input.GetKey(KeyCode.D))
        if (l_HorizontalAxis > 0.0f)
        {
            l_NewAngle = true;
            l_DesiredAngle = 90.0f;
            l_Direction.x = 1.0f;
        }
        //else if (Input.GetKey(KeyCode.A))
        else if (l_HorizontalAxis < 0.0f)
        {
            l_NewAngle = true;
            l_DesiredAngle = -90.0f;
            l_Direction.x = -1.0f;
        }
        //if (Input.GetKey(KeyCode.W))
        if (l_VerticalAxis > 0.0f)
        {
            if (l_NewAngle)
            {
                if (l_DesiredAngle == 90.0f)
                {
                    l_DesiredAngle = 45.0f;
                }
                if (l_DesiredAngle == -90.0f)
                {
                    l_DesiredAngle = -45.0f;
                }
            }
            else
            {
                l_DesiredAngle = 0.0f;
            }

            l_Direction.z = 1.0f;
        }
        //else if (Input.GetKey(KeyCode.S))
        else if (l_VerticalAxis < 0.0f)
        {
            if (l_NewAngle)
            {
                if (l_DesiredAngle == 90.0f)
                {
                    l_DesiredAngle = 135.0f;
                }
                if (l_DesiredAngle == -90.0f)
                {
                    l_DesiredAngle = 225.0f;
                }
            }
            else
            {
                l_DesiredAngle = 180.0f;
            }
            l_Direction.z = -1.0f;
        }

        m_Animator.SetFloat("Movement", l_Direction.magnitude);
        



        if (m_CanShootGrenade && l_AnimatorStateInfo.shortNameHash != l_ThrowGrenadeIdAnimation && m_Grenades > 0 && l_ThrowGrenadePressed)
        {
            m_Animator.SetBool("ThrowGrenade", l_ThrowGrenadePressed);
            ThrowGrenade();
        }
        else
        {
            m_Animator.SetBool("ThrowGrenade", false);
        }
        
        if (m_CurrentGrenade != null && l_AnimatorStateInfo.shortNameHash == l_ThrowGrenadeIdAnimation)
        {
            if (l_AnimatorStateInfo.normalizedTime >= 0.5f)
            {
                m_CurrentGrenade.GetComponent<Rigidbody>().isKinematic = false;
                Vector3 l_ForwardXZ = transform.forward;
                l_ForwardXZ.y = 0.0f;
                m_CurrentGrenade.GetComponent<Rigidbody>().velocity = m_GrenadeSpeedXZ * transform.forward + Vector3.up * m_GrenadeSpeedY;
                m_CurrentGrenade.GetComponent<GrenadeController>().enabled = true;
                m_CurrentGrenade = null;
            }
            else
            {
                m_CurrentGrenade.transform.position = m_OutputGrenadeTransform.position;
            }

        }
        if (l_AnimatorStateInfo.fullPathHash != l_ThrowGrenadeIdAnimation && m_CurrentGrenade == null && !m_CanShootGrenade)
        {
            m_CanShootGrenade = true;
        }

        //l_Direction.Normalize();
        //Vector3 l_Movement = l_Direction * m_Speed * Time.deltaTime;

        m_CharacterController.Move(Physics.gravity * Time.deltaTime);

        transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0.0f, l_DesiredAngle, 0.0f), Mathf.Min(1, Time.deltaTime/0.1f));
	}

    public void AddGrenade()
    {
        ++m_Grenades;
    }
    public void AddLife()
    {
        ++m_Lifes;
    }
    public void AddScore(int Amount)
    {
        m_Score += Amount;
    }
    public void Kill()
    {
        m_Animator.SetBool("Dead", true);
    }
    void RestartGame()
    {
        --m_Lifes;
        m_Animator.SetBool("Dead", false);
        m_CurrentShootTime = 0.0f;
        m_Grenades = 2;
        transform.rotation = m_StartRotation;
        transform.position = m_StartPosition;
        m_AmmoContainer.Restart();
        if (m_Lifes < 0)
        {
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            Camera.main.GetComponent<GameController>().RestartGame();
        }
        
    }
    bool CanShoot()
    {
        return m_CurrentShootTime <= 0.0f;
    }
    void Shoot(Vector3 Direction)
    {
        m_CurrentShootTime = m_ShootRate;

        m_AmmoContainer.AddAmmo(m_AmmoPrefab, m_OutputAmmo.position, Direction);
    }
    void ThrowGrenade()
    {
        --m_Grenades;
        m_CurrentGrenade = GameObject.Instantiate(m_GrenadePrefab, m_OutputGrenadeTransform.position, Quaternion.identity) as GameObject;
        m_CurrentGrenade.GetComponent<Rigidbody>().isKinematic = true;
        m_CurrentGrenade.GetComponent<GrenadeController>().enabled = false;
        m_CanShootGrenade = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explossion")
        {
            Kill();
        }
        else if (other.tag == "BossTrigger")
        {
            //m_GameController.SetBossState();
            Camera.main.GetComponent<GameController>().SetBossState();
        }
    }
}

