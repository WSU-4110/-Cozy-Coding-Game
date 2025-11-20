using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load scenes

public class MapButtonHandler : MonoBehaviour
{
    public string mapSceneName = "Map"; // Name of your Map scene

    // This function can be assigned to the button
    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
    }
}

