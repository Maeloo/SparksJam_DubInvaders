using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour, IPoolable
{
    private static int NUM = 0;

    [System.Serializable]
    public enum Type
    {
        Player,
        Enemy
    }

    [SerializeField] protected float m_minLifeTime;
    [SerializeField] protected Type m_Type;

    protected Rigidbody m_rigidbody;
    public Rigidbody Rigidbody { get { return m_rigidbody; } }

    protected float m_lastUse;

    #region Interface
    public void Create(IPoolable template)
    {
        NUM++;
        gameObject.name = ((MonoBehaviour)template).name + NUM;

        transform.position = ((MonoBehaviour)template).transform.position;
        transform.rotation = ((MonoBehaviour)template).transform.rotation;

        foreach (Transform child in ((MonoBehaviour)template).transform)
        {
            GameObject newChild = Instantiate<GameObject>(child.gameObject);
            newChild.transform.parent = transform;
            newChild.transform.localPosition = Vector3.zero;
            newChild.transform.localRotation = child.transform.localRotation;
        }

        m_minLifeTime = ((Bullet)template).m_minLifeTime;
        m_Type = ((Bullet)template).m_Type;

        m_rigidbody = gameObject.AddComponent<Rigidbody>();
        m_rigidbody.useGravity = false;

        gameObject.AddComponent<CapsuleCollider>().isTrigger = true;
    }

    public bool IsGameObject()
    {
        return true;
    }

    public bool IsReady()
    {
        return Time.time - m_lastUse > m_minLifeTime;
    }
    #endregion


    #region MonoBehaviour
    void Start()
    {
        m_lastUse = Time.time;
    }

    void Update()
    {
        m_rigidbody.angularVelocity = Vector3.zero;
    }

    void OnTriggerEnter(Collider col)
    {
        switch(m_Type)
        {
            case Type.Enemy:
                if(col.CompareTag("Player"))
                {
                    col.GetComponentInChildren<CockpitController>().OnDamage(1.0f);
                }
                break;

            case Type.Player:
                if (col.CompareTag("Enemy"))
                {
                    col.GetComponentInChildren<Fortress>().OnDamage(1.0f);
                }
                break;
        }
    }
    #endregion


    #region Shoot
    public void Shot()
    {
        m_lastUse = Time.time;
    }
    #endregion
}
