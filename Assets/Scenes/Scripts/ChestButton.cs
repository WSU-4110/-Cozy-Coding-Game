using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestButton : MonoBehaviour
{
    [Header("Level Settings")]
    public string levelSceneName;
    public int levelOrder;
    
    [Header("Visual Settings")]
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    [Header("Testing Mode")]
    public bool testingMode = true;  // Set this to true for testing!
    
    private Button button;
    private Image buttonImage;
    private bool isUnlocked = false;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        
        LoadChestState();
        UpdateButtonState();
        
        button.onClick.AddListener(OnChestClicked);
    }

    private void LoadChestState()
    {
        if (testingMode)
        {
            // ALL CHESTS UNLOCKED FOR TESTING
            isUnlocked = true;
        }
        else
        {
            // NORMAL PROGRESSION SYSTEM
            int highestCompleted = PlayerPrefs.GetInt("HighestCompletedLevel", 0);
            isUnlocked = (levelOrder <= highestCompleted + 1);
        }
    }

    private void UpdateButtonState()
    {
        if (buttonImage != null && lockedSprite != null && unlockedSprite != null)
        {
            buttonImage.sprite = isUnlocked ? unlockedSprite : lockedSprite;
            buttonImage.color = Color.white;
        }
        button.interactable = isUnlocked;
    }

    private void OnChestClicked()
    {
        if (isUnlocked && !string.IsNullOrEmpty(levelSceneName) && levelSceneName != "TEMP")
        {
            Debug.Log($"Loading scene: {levelSceneName}");
            SceneManager.LoadScene(levelSceneName);
        }
        else if (levelSceneName == "TEMP")
        {
            Debug.LogWarning("Level for this chest not set up yet!");
        }
    }

    public void RefreshChestState()
    {
        LoadChestState();
        UpdateButtonState();
    }
}