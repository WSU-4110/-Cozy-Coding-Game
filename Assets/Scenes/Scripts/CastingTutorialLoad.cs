using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// </summary>
public class CastingTutorialLoad : MonoBehaviour
{
   
    public void LoadCastingLevel(string sceneName) 
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name cannot be empty. Check the Button's OnClick() argument.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}