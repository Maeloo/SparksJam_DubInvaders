using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailwayManager : MonoBehaviour {

    private Dictionary<string, List<Transform>> m_Railways;


    void Awake()
    {
        m_Railways = new Dictionary<string, List<Transform>>();

        Rail[] rails = FindObjectsOfType<Rail>();

        foreach(Rail rail in rails)
        {
            if (!m_Railways.ContainsKey(rail.RailwayName))
                m_Railways[rail.RailwayName] = new List<Transform>();

            m_Railways[rail.RailwayName].Add(rail.transform);
        }

        foreach(KeyValuePair<string, List<Transform>> Railway in m_Railways)
        {
            for(byte idx = 0; idx < Railway.Value.Count; ++idx)
            {
                if(Railway.Value[idx].GetComponent<Rail>().Position != idx)
                {
                    for (byte idx_ = 0; idx_ < Railway.Value.Count; ++idx_)
                    {
                        if (Railway.Value[idx_].GetComponent<Rail>().Position == idx)
                        {
                            Transform temp = Railway.Value[idx];
                            Railway.Value[idx] = Railway.Value[idx_];
                            Railway.Value[idx_] = temp;
                        }
                    }
                }
            }
        }
    }


    public bool GetDataOnRailway(string name, float percent, out Vector3 position, out Vector3 direction)
    {
        position = direction = Vector3.zero;

        List<Transform> railway;
        if (GetRailway(name, out railway))
        {
            int idx0 = (int)Mathf.Floor((railway.Count - 1) * percent),
                idx1 = idx0 < railway.Count - 1 ? idx0 + 1 : idx0;

            position = iTween.PointOnPath(railway.ToArray(), percent);

            //percent = (position - railway[idx0].position).magnitude / (railway[idx0].position - railway[idx1].position).magnitude;
            //direction = Vector3.Lerp(railway[idx0].forward, railway[idx1].forward, percent);

            return true;
        }   

        return false;
    }


    private bool GetRailway(string name, out List<Transform> rails)
    {
        rails = null;
        if(m_Railways.ContainsKey(name))
        {
            rails = m_Railways[name];

            return true;
        }

        return false;
    }
}
