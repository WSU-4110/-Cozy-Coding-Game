using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change scenes

public class SceneLoader : MonoBehaviour
{
    // Loads the scene by name
    public void LoadVariableNamesScene()
    {
        SceneManager.LoadScene("Variable Names"); 
    }

}
