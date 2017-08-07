using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float m_Speed;
    CharacterController m_CharacterController;

	// Use this for initialization
	void Start () 
	{
        m_CharacterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 l_Direction = Vector3.zero;
        float l_DesiredAngle = transform.rotation.eulerAngles.y;
        bool l_NewAngle = false;

        if (Input.GetKey(KeyCode.D))
        {
            l_NewAngle = true;
            l_DesiredAngle = 90.0f;
            l_Direction.x = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            l_NewAngle = true;
            l_DesiredAngle = -90.0f;
            l_Direction.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.W))
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
        else if (Input.GetKey(KeyCode.S))
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

        l_Direction.Normalize();
        Vector3 l_Movement = l_Direction * m_Speed * Time.deltaTime;

        m_CharacterController.Move(l_Movement);

        transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0.0f, l_DesiredAngle, 0.0f), Mathf.Min(1, Time.deltaTime/0.1f));
	}
}
