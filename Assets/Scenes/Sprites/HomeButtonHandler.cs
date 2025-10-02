using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene switching

public class HomeButtonHandler : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void GoToHome()
    {
        // Replace "HomeScene" with the exact name of your home screen scene
        SceneManager.LoadScene("Home");
    }
}
