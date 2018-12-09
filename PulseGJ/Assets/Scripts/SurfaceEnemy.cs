using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SurfaceEnemy : Enemy {

    [SerializeField] protected string m_RailwayName;

    protected RailwayManager m_RWManager;

    protected Transform m_Visual;

    protected float m_Percent;

    void Start()
    {
        m_RWManager = FindObjectOfType<RailwayManager>();
        m_Visual = transform.FindChild("Visual");
        m_Percent = .0f;
    }


    void Update()
    {
        m_Percent = m_Percent + m_Speed > 1.0f ? .0f : m_Percent + m_Speed;

        Vector3 pos,
                fwd;

        if (m_RWManager.GetDataOnRailway(m_RailwayName, m_Percent, out pos, out fwd))
        {
            transform.position = pos;
            //m_Visual.forward = fwd;
        }
    }
}
