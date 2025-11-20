using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene loading specifically for navigation from the Numbers Level to the Map.
/// This script is attached to a SceneManagers or an empty GameObject in the scene.
/// </summary>
public class NumbersMapLoad : MonoBehaviour
{
    // Make sure the method is public so it can be accessed by the Button's OnClick event.
    // We will use a string parameter to make the script flexible, allowing it to load any scene.
    public void LoadSceneByName(string sceneName)
    {
        // Safety check to ensure the scene exists and is in the build settings.
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Transitioning from Numbers Level to scene: {sceneName}");
        }
        else
        {
            Debug.LogError($"ERROR: Scene '{sceneName}' cannot be loaded. Check that the scene is spelled correctly ('Map') and is added to File > Build Settings.");
        }
    }
}