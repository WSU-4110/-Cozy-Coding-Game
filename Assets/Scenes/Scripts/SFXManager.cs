using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance; // Singleton so we can call it easily
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
