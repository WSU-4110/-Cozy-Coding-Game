using UnityEngine;
using TMPro;

public class SaveManager : MonoBehaviour
{
    // Singleton instance
    public static SaveManager Instance { get; private set; }

    [Header("Optional UI Feedback")]
    public TMP_Text saveMessageText;

    public int currentLevel = 1;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        // Enforce only one SaveManager across all scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Save player progress and settings
    public void SaveGame()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();

        Debug.Log("Game Saved!");

        if (saveMessageText != null)
        {
            saveMessageText.text = "Game Saved!";
            CancelInvoke(nameof(ClearMessage));
            Invoke(nameof(ClearMessage), 2f);
        }
    }

    // Load saved progress and settings
    public void LoadGame()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);

        Debug.Log("Game Loaded!");
    }

    // Autosave function that can be triggered after events
    public void AutoSave()
    {
        SaveGame();
        Debug.Log("Autosave Complete!");
    }

    private void ClearMessage()
    {
        if (saveMessageText != null)
            saveMessageText.text = "";
    }
}
