using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class TreasureClick : MonoBehaviour
{
    [Tooltip("Syntax Level")]
    public string sceneToLoad = "Syntax Level";

    bool isLoading = false;

    void OnMouseDown()
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}
