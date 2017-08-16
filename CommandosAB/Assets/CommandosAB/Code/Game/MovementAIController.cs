﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CAIController : MonoBehaviour
{
    public virtual void Kill()
    {
        Debug.LogError("this method must be overrided");
    }
}

public class MovementAIController : CAIController
{
    PlayerController m_PlayerController;
    public GameObject m_AmmoPrefab;
    AmmoContainer m_AmmoContainer;
    public float m_BaseTimeToShoot = 5.0f;
    public float m_RandomTimeToShoot = 3.0f;
    float m_CurrentTime;
    public Transform m_AmmoOutputTransform;
    Animator m_Animator;
    CharacterController m_CharacterController;
    NavMeshAgent m_NavMeshAgent;
    bool m_DestinationSet;
    public float m_Angle = 40.0f;
    public float m_BaseDistance = 5.0f;
    public float m_RandomDistance = 3.0f;
    Vector3 m_Destination;

    // Use this for initialization
    void Start ()
	{
        m_CharacterController = GetComponent<CharacterController>();
        m_AmmoContainer = GetComponent<AmmoContainer>();
        m_PlayerController = Camera.main.GetComponent<CameraController>().m_PlayerTransform.GetComponent<PlayerController>();
        m_Animator = GetComponent<Animator>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        CalcNextShootTime();
        m_DestinationSet = false;

    }

	// Update is called once per frame
	void Update ()
	{
        if (m_Animator.GetBool("Dead"))
        {
            return;
        }
        if (!IsOnScreen())
        {
            return;
        }

        if (!m_DestinationSet)
        {
            SetDestination();
        }

        Debug.DrawRay(m_Destination, Vector3.up * 3.0f, Color.red);

        m_CurrentTime -= Time.deltaTime;
        if (m_CurrentTime <= 0.0f)
        {
            Shoot();
        }

        float l_DesiredVelocity = m_NavMeshAgent.desiredVelocity.magnitude;
        if (l_DesiredVelocity < 0.5f)
        {
            Vector3 l_Direction = m_PlayerController.transform.position - transform.position;
            l_Direction.y = 0.0f;
            l_Direction.Normalize();
            transform.forward = l_Direction;
        }
        m_Animator.SetFloat("Speed", m_NavMeshAgent.desiredVelocity.magnitude);
	}

    void CalcNextShootTime()
    {
        m_CurrentTime = m_BaseTimeToShoot + Random.value * m_RandomTimeToShoot;
    }

    void Shoot()
    {
        m_AmmoContainer.AddAmmo(m_AmmoPrefab, m_AmmoOutputTransform.position, transform.forward);
        CalcNextShootTime();

    }
    public override void Kill()
    {
        m_Animator.SetBool("Dead", true);
        m_CharacterController.enabled = false;
    }
    bool IsOnScreen()
    {
        Vector3 l_Position = Camera.main.WorldToViewportPoint(transform.position);
        return l_Position.y >= 0.0f && l_Position.y <= 1.0f;
    }

    void SetDestination()
    {
        float l_Angle = Random.Range(-m_Angle, m_Angle) * Mathf.Deg2Rad;
        float l_Distance = m_BaseDistance + Random.value * m_RandomDistance;
        m_Destination = m_PlayerController.transform.position + new Vector3(Mathf.Cos(l_Angle + Mathf.PI * 0.5f), 0.0f, Mathf.Sin(l_Angle + Mathf.PI * 0.5f))*l_Distance;
        m_DestinationSet = true;
        m_NavMeshAgent.SetDestination (m_Destination);
        m_NavMeshAgent.updateRotation = true;
        m_NavMeshAgent.updatePosition = false;
    }
}