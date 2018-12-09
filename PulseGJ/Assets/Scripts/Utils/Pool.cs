using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool<T> where T : Component, IPoolable, new()
{
    private List<T> m_objects = new List<T>();

    public Pool(int size, T template)
    {
        for(byte idx = 0; idx < size; ++idx)
        {
            T newObject = null;

            if (template.IsGameObject())
            {
                GameObject newGameObject = new GameObject();
                newObject = newGameObject.AddComponent<T>();
            }
            else
            {
                newObject = new T();
            }

            newObject.Create(template);

            m_objects.Add(newObject);
        }
    }


    public void SetParent(Transform parent)
    {
        for (byte idx = 0; idx < m_objects.Count; ++idx)
        {
            m_objects[idx].transform.parent = parent;
        }
    }


    public bool GetObject(out T nextObject)
    {
        nextObject = default(T);

        for (byte idx = 0; idx < m_objects.Count; ++idx)
        {
            if(m_objects[idx].IsReady())
            {
                nextObject = m_objects[idx];
                return true;
            }
        }

        return false;
    }
}
