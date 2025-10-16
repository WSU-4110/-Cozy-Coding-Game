using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TreasureChestInteraction : MonoBehaviour
{
    // Drag your whole 'speec_o' GameObject here in the Inspector
    public GameObject speechBubblePanel; 
    
    // Drag the TextMeshPro component (the text inside the bubble) here
    public TextMeshProUGUI speechText; 

    // STATE VARIABLE: Tracks if the dialogue is currently on screen
    private bool dialogueIsActive = false;

    // The message you want to display
    private string startMessage = "Hello! You are starting level 1, Syntax Level. Good Luck!";
    
    // The name of the scene you want to load
    public string levelOneSceneName = "Syntax Level"; 

    // Called when the mouse button is pressed while over this object's 2D collider
    void OnMouseDown()
    {
        // Check the current state of the interaction

        if (!dialogueIsActive)
        {
            // --- FIRST CLICK: Show the dialogue ---
            ShowDialogue();
            dialogueIsActive = true; // Set the state to true so the next click loads the level
        }
        else 
        {
            // --- SECOND CLICK: Load the level ---
            LoadLevelOne();
        }
    }

    void ShowDialogue()
    {
        // 1. Set the message text
        if (speechText != null)
        {
            speechText.text = startMessage;
        }
        
        // 2. Activate the whole 'speec_o' object (makes the bubble and text appear)
        if (speechBubblePanel != null)
        {
            // Make sure the bubble is visible
            speechBubblePanel.SetActive(true);
        }
    }
    
    void LoadLevelOne()
    {
        // 1. (Optional) Hide the bubble before loading the scene
        if (speechBubblePanel != null)
        {
            speechBubblePanel.SetActive(false);
        }

        // 2. Load the level
        SceneManager.LoadScene(levelOneSceneName);
    }
}