using UnityEngine;
using UnityEngine.SceneManagement; // Needed to load scenes

public class HomeButtonHandler : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene(3);
    }
}
