﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public Transform m_PlayerTransform;
    Vector3 m_OffsetCamera;
    public float m_XDivider = 1.0f;
    float m_MinZCamera;

	// Use this for initialization
	void Start () 
	{
        m_OffsetCamera = transform.position - m_PlayerTransform.position;
        m_MinZCamera = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 l_CameraPosition = m_PlayerTransform.position + m_OffsetCamera;
        l_CameraPosition.x /= m_XDivider;
        l_CameraPosition.z = Mathf.Max(m_MinZCamera, l_CameraPosition.z);
        m_MinZCamera = l_CameraPosition.z;
        transform.position = l_CameraPosition;
	}
}
