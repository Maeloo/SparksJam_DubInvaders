using UnityEngine;
using System.Collections;

public class Rail : MonoBehaviour {

    [SerializeField] private string m_RailwayName;
    [SerializeField] private int m_RailwayPos;

    public string RailwayName { get { return m_RailwayName; } }
    public int Position { get { return m_RailwayPos; } }

}
