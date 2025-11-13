using UnityEngine;
using UnityEngine.SceneManagement;

public class NumbersSceneLoad : MonoBehaviour
{
  public void LoadNumbersLevel(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name cannot be empty. Check the Button's OnClick() argument.");
            return;
        }

      
        SceneManager.LoadScene(sceneName);
    }
}