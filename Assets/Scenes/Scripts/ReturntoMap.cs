using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMap : MonoBehaviour
{
    
    public string mapSceneName = "Map";

    public void GoToMap()
    {
        SceneManager.LoadScene(mapSceneName);
    }
}
