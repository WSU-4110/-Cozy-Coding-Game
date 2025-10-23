using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TreasureChestInteraction : MonoBehaviour
{
    public GameObject speechBubblePanel; 
    public TextMeshProUGUI speechText; 

    private bool dialogueIsActive = false;
    private string startMessage = "Hello! You are starting level 1, Syntax Level. Good Luck!";
    public string levelOneSceneName = "Syntax Level"; 

    void OnMouseDown()
    {
        if (!dialogueIsActive)
        {
            // --- FIRST CLICK: Show the speech bubble ---
            ShowDialogue();
            dialogueIsActive = true;
        }
        else
        {
            // --- SECOND CLICK: Load the next level ---
            LoadLevelOne();
        }
    }

    void ShowDialogue()
    {
        if (speechText != null)
            speechText.text = startMessage;
        
        if (speechBubblePanel != null)
            speechBubblePanel.SetActive(true);
    }
    
    void LoadLevelOne()
    {
        if (speechBubblePanel != null)
            speechBubblePanel.SetActive(false);

        SceneManager.LoadScene(levelOneSceneName);
    }
}
