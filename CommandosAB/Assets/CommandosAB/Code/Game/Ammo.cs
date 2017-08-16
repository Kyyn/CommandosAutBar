using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public enum TAmmoType 
    {
        PLAYER=0,
        ENEMY
    }
    public TAmmoType m_Type;
    public float m_Speed = 10.0f;
    public float m_LifeTime = 3.0f;
    public float m_CurrentTime = 0.0f;
	// Use this for initialization
	void Start ()
	{
        m_CurrentTime = 0.0f;
	}

	// Update is called once per frame
	void Update ()
	{
        m_CurrentTime += Time.deltaTime;
		if (m_CurrentTime >= m_LifeTime)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
    public void Shoot (Vector3 Direction)
    {
        Rigidbody l_rigidbody = GetComponent<Rigidbody>();
        l_rigidbody.velocity = Direction * m_Speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && m_Type == TAmmoType.ENEMY)
        {
            collision.gameObject.GetComponent<PlayerController>().Kill();
        }
        else if (collision.gameObject.tag == "Enemy" && m_Type == TAmmoType.PLAYER)
        {
            collision.gameObject.GetComponent<CAIController>().Kill();
        }
        GameObject.Destroy(gameObject);
    }
}
