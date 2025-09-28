using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        // Loads the *next* scene in Build Settings order
        SceneManager.LoadScene("Game Scene1");
    }
}