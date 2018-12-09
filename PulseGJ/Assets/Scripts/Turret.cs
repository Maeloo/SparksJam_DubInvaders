using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	[SerializeField] protected Canon m_canon;

    protected Transform m_target;


    void Start()
    {
        m_target = FindObjectOfType<PlayerController>().transform;
    }


    protected void Update()
    {
        if (m_target)
        {
            m_canon.transform.LookAt(m_target);
        }

        m_canon.Shoot(transform);
    }

}
