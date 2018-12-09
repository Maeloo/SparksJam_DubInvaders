using UnityEngine;
using System.Collections;

public class Fortress : SurfaceEnemy {

    [SerializeField]
    private GameObject m_youWin;

    public void OnDamage(float damage)
    {
        m_BaseLife -= damage;

        if(m_BaseLife < .0f)
        {
            m_youWin.SetActive(true);
            Time.timeScale = .0f;
        }
    }
}
