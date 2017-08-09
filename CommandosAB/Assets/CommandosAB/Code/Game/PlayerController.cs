using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float m_Speed;
    CharacterController m_CharacterController;
    Animator m_Animator;

    public int m_Score = 0;
    public int m_Grenades = 2;
    public int m_Lifes = 3;


    // Use this for initialization
    void Start () 
	{
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 l_Direction = Vector3.zero;
        float l_DesiredAngle = transform.rotation.eulerAngles.y;
        bool l_NewAngle = false;
        float l_HorizontalAxis = Input.GetAxis("Horizontal");
        float l_VerticalAxis = Input.GetAxis("Vertical");
        bool l_FirePressed = Input.GetButton("Fire1");
        bool l_ThrowGrenadePressed = Input.GetButton("Fire2");

        m_Animator.SetBool("Shoot", l_FirePressed);
        m_Animator.SetBool("ThrowGrenade", l_ThrowGrenadePressed);
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

        //l_Direction.Normalize();
        //Vector3 l_Movement = l_Direction * m_Speed * Time.deltaTime;

        //m_CharacterController.Move(l_Movement);

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
}
