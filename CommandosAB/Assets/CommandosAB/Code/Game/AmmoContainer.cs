using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoContainer : MonoBehaviour
{
    public GameObject m_DestroyObjects;
    List<Ammo> m_Ammo;

	// Use this for initialization
	void Start ()
	{
        m_Ammo = new List<Ammo>();

	}

	// Update is called once per frame
	void Update ()
	{
		
	}
    public void AddAmmo (GameObject AmmoPrefab, Vector3 Position, Vector3 Direction)
    {
        GameObject l_AmmoGO = GameObject.Instantiate(AmmoPrefab, Position, Quaternion.identity) as GameObject;
        l_AmmoGO.transform.parent = m_DestroyObjects.transform;
        Ammo l_Ammo = l_AmmoGO.GetComponent<Ammo>();
        l_Ammo.Shoot(Direction);
        m_Ammo.Add(l_Ammo);
    }
    public void Restart()
    {
        foreach (Ammo l_Ammo in m_Ammo)
        {
            if (l_Ammo != null)
            {
                GameObject.Destroy(l_Ammo.gameObject);
            }
        }
        m_Ammo = new List<Ammo>();
    }
}
