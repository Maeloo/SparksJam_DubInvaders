using UnityEngine;
using System.Collections;
using Leap.Unity;
using Leap;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Canon m_canon;

    [SerializeField]
    private CockpitController m_CockpitController;

    [SerializeField]
    private ParticleSystem m_fx;

    private LeapProvider provider;    

    private Quaternion m_lastRotation;

    private bool m_IsGrabing;

    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;

        m_lastRotation = Quaternion.identity;
        m_IsGrabing = false;
    }

		
	void Update ()
    {
        KeyboardInput();
        MouseInput();
        LeapInput();
    }



    void LeapInput()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if(!m_IsGrabing && hand.GrabStrength == 1.0f)
            {
                m_lastRotation = hand.Rotation.ToQuaternion();
                m_IsGrabing = true;
            }

            if (hand.IsRight && m_IsGrabing)
            {
                Quaternion qrot = Quaternion.Inverse(hand.Rotation.ToQuaternion()) * m_lastRotation;
                Vector3 vrot = qrot.eulerAngles;
                m_CockpitController.Navigator.Rotate(-vrot);
                m_lastRotation = hand.Rotation.ToQuaternion();
            }

            m_IsGrabing = hand.GrabStrength == 1.0f;
        }
    }


    void MouseInput()
    {
        if(Input.GetMouseButton(0))
        {
            //m_fx.Play();
            m_canon.Shoot(Camera.main.transform);
        }
    }


    void KeyboardInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            m_CockpitController.Navigator.transform.Rotate(Vector3.right, 10.0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_CockpitController.Navigator.transform.Rotate(Vector3.up, -10.0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_CockpitController.Navigator.transform.Rotate(Vector3.right, -10.0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_CockpitController.Navigator.transform.Rotate(Vector3.up, 10.0f);
        }
    }
}
