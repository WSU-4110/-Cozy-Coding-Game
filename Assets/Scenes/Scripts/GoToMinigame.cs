using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMiniGame : MonoBehaviour
{
    public string miniGameSceneName = "String minigame1";

    public void LoadMiniGame()
    {
        SceneManager.LoadScene(miniGameSceneName);
    }
}
