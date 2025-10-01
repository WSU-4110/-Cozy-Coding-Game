using UnityEngine;

public class MusicPlayerController : MonoBehaviour
{
    private static MusicPlayerController instance;

    void Awake()
    {
        // If a MusicPlayer already exists, destroy this duplicate
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Make this the main instance and don't destroy it when scenes change
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
