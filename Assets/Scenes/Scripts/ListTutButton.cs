using UnityEngine;
using UnityEngine.SceneManagement;


public class ListTutButton : MonoBehaviour
{
    public void LoadLevel1()
    {
        // Loads first Python List level
        SceneManager.LoadScene("ListLevel1");
    }
}
