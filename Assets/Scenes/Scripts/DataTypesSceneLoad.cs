using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTypesSceneLoad : MonoBehaviour
{
 public void LoadLevel3(string sceneName)
    {
        // SceneManager.LoadScene() takes the name of the scene you want to load
        SceneManager.LoadScene(sceneName);
    }
}