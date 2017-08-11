using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject m_ExplossionPrebaf;
    float m_CurrentTime = 0.0f;
    public float m_TimeToExplode = 0.5f;

	
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
        m_CurrentTime += Time.deltaTime;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (m_CurrentTime >= m_TimeToExplode)
        {
            GameObject l_Explossion = GameObject.Instantiate(m_ExplossionPrebaf, transform.position, Quaternion.identity) as GameObject;
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(l_Explossion, 3.0f);
        }


    }
}
