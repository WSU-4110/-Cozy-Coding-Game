using UnityEngine;
using TMPro;  

public class SaveManager : MonoBehaviour
{
    [Header("Optional UI Feedback")]
    public TMP_Text saveMessageText; 

    public int currentLevel = 1;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

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
            Invoke(nameof(ClearMessage), 2f); // Clears after 2 seconds
        }
    }

    private void ClearMessage()
    {
        if (saveMessageText != null)
            saveMessageText.text = "";
    }
}
