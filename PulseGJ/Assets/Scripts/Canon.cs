using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour {

    [SerializeField] protected int m_maxAmmos;

    [SerializeField] protected Bullet m_ammoPrefab;

    [SerializeField] protected float m_force;
    [SerializeField] protected float m_attackRate;

    protected Pool<Bullet> m_ammunitions;

    protected float m_lastShot;


    void Start()
    {
        GameObject ammoStock = new GameObject();
        ammoStock.name = gameObject.name + "_Ammos";

        m_ammunitions = new Pool<Bullet>(m_maxAmmos, m_ammoPrefab);
        m_ammunitions.SetParent(ammoStock.transform);

        m_lastShot = Time.time;
    }


    #region Shoot
    public void Shoot(Transform realCanon)
    {
        if((Time.time - m_lastShot) > m_attackRate)
        {
            Bullet bullet;
            if (m_ammunitions.GetObject(out bullet))
            {
                bullet.transform.position = transform.position;
                bullet.transform.forward = realCanon.transform.forward;

                bullet.Rigidbody.velocity = Vector3.zero;
                bullet.Rigidbody.angularVelocity = Vector3.zero;
                bullet.Rigidbody.AddForce(realCanon.transform.forward * m_force);

                bullet.Shot();
            }

            m_lastShot = Time.time;
        }        
    }
    #endregion

}
