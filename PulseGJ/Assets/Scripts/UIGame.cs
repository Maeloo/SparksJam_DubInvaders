using UnityEngine;
using System.Collections;

public class UIGame : MonoBehaviour {

	public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
