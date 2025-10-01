using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;   // Singleton instance

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // persists across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure we have an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    // Play a one-shot sound effect
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Adjust SFX volume (used by SettingsManager)
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
