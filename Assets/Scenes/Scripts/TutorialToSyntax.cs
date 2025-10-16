using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialToSyntax : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void toSyntaxLevel()
    {
        SceneManager.LoadScene("Syntax Level");
    }
}
