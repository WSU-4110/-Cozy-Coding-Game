using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDecode : MonoBehaviour
{
    [Header("Name of Scene to Load")]
    public string nextSceneName = "String decode level";   

    public void Go()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
