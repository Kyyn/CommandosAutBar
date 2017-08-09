using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour 
{
    PlayerController m_Player;

    public enum TItemType
    {
        GRENADE=0,
        LIFE,
        SCORE
    }

    public TItemType m_Type;

	// Use this for initialization
	void Start () 
	{
        m_Player = Camera.main.GetComponent<CameraController>().m_PlayerTransform.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (m_Type)
            {
                case TItemType.GRENADE:
                    m_Player.AddGrenade();
                    break;
                case TItemType.LIFE:
                    m_Player.AddLife();
                    break;
                case TItemType.SCORE:
                    m_Player.AddScore(1000);
                    break;
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}
