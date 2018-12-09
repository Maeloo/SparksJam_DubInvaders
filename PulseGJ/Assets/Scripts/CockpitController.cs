using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

public class CockpitController : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;
    [SerializeField] private float m_StepInterval;
    [SerializeField] private Transform m_Navigator;
    [SerializeField] private Orbit m_orbit;
    [SerializeField] private float m_life = 20.0f;
    [SerializeField] private Text m_lifeText;
    [SerializeField] private GameObject m_gameOver;

    public Transform Navigator { get { return m_Navigator; } }

    private Camera m_Camera;
    private float m_YRotation;
    private Vector2 m_Input;


    // Use this for initialization
    private void Start()
    {
        m_Camera = Camera.main;
        m_MouseLook.Init(transform, m_Camera.transform);
    }


    // Update is called once per frame
    private void Update()
    {
        RotateView();

        m_lifeText.text = m_life.ToString();
    }


    private void FixedUpdate()
    {
        GetInput();

        m_MouseLook.UpdateCursorLock();

        m_orbit.SetAxis(m_Navigator.up);
    }


    private void GetInput()
    {
        // Read input
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        m_Input = new Vector2(horizontal, vertical);

        // normalize input if it exceeds 1 in combined length:
        if (m_Input.sqrMagnitude > 1)
        {
            m_Input.Normalize();
        }
    }


    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }


    public void OnDamage(float damage)
    {
        m_life -= damage;

        if(m_life < .0f)
        {
            m_gameOver.SetActive(true);
            Time.timeScale = .0f;
        }

        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash(
            "amount", Camera.main.transform.up * .5f, 
            "time", .16f));

        Invoke("ResetPos", .3f);
    }

    public void ResetPos()
    {
        Camera.main.transform.localPosition = new Vector3(.0f, .0f, 0.1f);
    }

}
