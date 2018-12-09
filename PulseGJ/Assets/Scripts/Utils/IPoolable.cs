using UnityEngine;
using System.Collections;

public interface IPoolable {

    void Create(IPoolable template);
    bool IsGameObject();
    bool IsReady();

}
