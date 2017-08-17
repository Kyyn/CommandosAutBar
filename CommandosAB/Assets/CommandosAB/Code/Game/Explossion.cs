using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explossion : MonoBehaviour
{
    SphereCollider m_SphereCollider;
    float m_CurrentTime;

    // Use this for initialization
    void Start ()
	{
        m_SphereCollider = GetComponent<SphereCollider>();
        m_SphereCollider.radius = 0.0f;
        m_CurrentTime = 0.0f;

    }

	// Update is called once per frame
	void Update ()
	{
        m_CurrentTime += Time.deltaTime;
        float l_Pct = Mathf.Min(1.0f, m_CurrentTime / 1.0f);
        m_SphereCollider.radius = l_Pct * 5.0f;
	}
}
