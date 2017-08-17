using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAIController : CAIController
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

    // Use this for initialization
    void Start ()
	{
        m_CharacterController = GetComponent<CharacterController>();
        m_AmmoContainer = GetComponent<AmmoContainer>();
        m_PlayerController = Camera.main.GetComponent<CameraController>().m_PlayerTransform.GetComponent<PlayerController>();
        m_Animator = GetComponent<Animator>();
        CalcNextShootTime();

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
        m_CurrentTime -= Time.deltaTime;
        if (m_CurrentTime <= 0.0f)
        {
            Shoot();
        }

        Vector3 l_Direction = m_PlayerController.transform.position - transform.position;
        l_Direction.y = 0.0f;
        l_Direction.Normalize();
        transform.forward = l_Direction;
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
        m_PlayerController.AddScore(100);
    }
    bool IsOnScreen()
    {
        Vector3 l_Position = Camera.main.WorldToViewportPoint(transform.position);
        return l_Position.y >= 0.0f && l_Position.y <= 1.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explossion")
        {
            Debug.Log("nova2");
            Kill();
        }
    }
}
