using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Planet : MonoBehaviour {

    void Update()
    {
        foreach (Transform child in transform)
        {
            child.up = (child.position - transform.position).normalized;
            child.position = child.up * 50;
        }
    }

}
