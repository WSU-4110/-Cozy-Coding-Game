using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    public string mapSceneName = "Map";
    public Button mapButton;

    void Start()
    {
        mapButton.onClick.AddListener(GoToMap);
    }

    void GoToMap()
    {
        SceneManager.LoadScene(mapSceneName);
    }
}

