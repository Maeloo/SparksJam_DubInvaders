using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    [SerializeField] private Transform m_targetPivot;

    [SerializeField] private float m_Speed  = 1.0f;
    [SerializeField] private float m_DescentSpeed  = 1.0f;
    [SerializeField] private float m_Radius = 150.0f;

    private GameObject m_realPivot;

    private Vector3 m_axis = Vector3.up;
    private Vector3 m_direction;


    void Start()
    {
        m_realPivot = new GameObject();
        m_realPivot.name = gameObject.name + "_CenterOfGravity";

        transform.parent = m_realPivot.transform;
    }

    void Update()
    {
        m_realPivot.transform.position = m_targetPivot.position;
        m_realPivot.transform.Rotate(m_axis, m_Speed * Time.deltaTime);

        m_direction = (transform.position - m_targetPivot.position).normalized;

        m_Radius = Mathf.Lerp(m_Radius, m_Radius - m_DescentSpeed, Time.deltaTime);

        transform.position = m_targetPivot.position + m_direction * m_Radius;
    }

    public void SetAxis(Vector3 axis)
    {
        m_axis = axis;
    }
}
