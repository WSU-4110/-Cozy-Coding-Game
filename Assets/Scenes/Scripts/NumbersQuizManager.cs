using UnityEngine;
using UnityEngine.UI;
using TMPro; // Needed for TextMeshPro
using System.Collections.Generic;
using System.Collections; // Needed for Coroutines

// Ensure you have a separate file named QuizQuestionData.cs with the data structure!

public class NumbersQuizManager : MonoBehaviour
{
    // --- Quiz Data Structure ---
    [Header("Quiz Data")]
    public QuizQuestionData[] allQuestions;
    private int currentQuestionIndex = 0;

    // --- References to UI Components ---
    [Header("UI References")]
    public TextMeshProUGUI questionText;
    
    // Public references for the buttons
    public Button intButton;
    public Button floatButton;
    public Button longButton;
    public Button complexButton; 

    private List<Button> allAnswerButtons;

    // The feedback text object and component
    public GameObject feedbackTextObject; 
    public TextMeshProUGUI feedbackText; 

    // --- Configuration ---
    [Header("Configuration")]
    public float feedbackDisplayTime = 1.5f; // How long to wait before showing the next question


    void Start()
    {
        // Initialize the list of buttons
        // It's crucial to check for nulls here, especially for the complex button
        allAnswerButtons = new List<Button>();
        if (intButton != null) allAnswerButtons.Add(intButton);
        if (floatButton != null) allAnswerButtons.Add(floatButton);
        if (longButton != null) allAnswerButtons.Add(longButton);
        if (complexButton != null) allAnswerButtons.Add(complexButton);

        // 1. Ensure the feedback text is hidden at the start
        feedbackTextObject.SetActive(false);

        // 2. Assign the click function to each button DYNAMICALLY.
        // This is the CRUCIAL FIX. We use a function that gets the button's text
        // at the moment of the click, so it works for all questions.
        foreach (var button in allAnswerButtons)
        {
            // First, clear any old listeners that might exist from previous setups
            button.onClick.RemoveAllListeners();
            
            // Add the new dynamic listener
            button.onClick.AddListener(() =>
            {
                // Find the TextMeshProUGUI child component and pass its text
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    CheckAnswer(buttonText.text);
                }
                else
                {
                    Debug.LogError($"Button {button.name} is missing a TextMeshProUGUI child component! Cannot check answer.");
                }
            });
        }
        
        // 3. Start the quiz by displaying the first question
        if (allQuestions.Count > 0) // Changed to .Count for array, though .Length works too
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.LogError("No questions defined in the Quiz Manager! Please populate the 'All Questions' array in the Inspector.");
        }
    }

    // --- Main Logic Methods ---

    public void CheckAnswer(string selectedAnswer)
    {
        // Stop processing multiple clicks until feedback is done
        DisableButtons(); 
        
        // Safety check to ensure we aren't accessing an out-of-bounds question
        if (currentQuestionIndex >= allQuestions.Length)
        {
            return;
        }

        // Get the correct answer from the current question's data
        QuizQuestionData currentQ = allQuestions[currentQuestionIndex];
        string correctAnswer = currentQ.CorrectAnswer;

        // Check if the clicked answer matches the correct answer
        // Note: ToLower() ensures "Float" and "float" both count as correct
        if (selectedAnswer.ToLower() == correctAnswer.ToLower())
        {
            feedbackText.text = "CORRECT!";
            feedbackText.color = Color.green;
            
            // Advance to the next question
            currentQuestionIndex++;
            
            // Show feedback and then move to the next question
            StartCoroutine(ShowFeedbackAndAdvance());
        }
        else
        {
            feedbackText.text = "INCORRECT. Try again.";
            feedbackText.color = Color.red; 
            
            // Show feedback briefly, then re-enable buttons for another try
            StartCoroutine(ShowFeedbackAndReEnable());
        }

        // Show the feedback text
        feedbackTextObject.SetActive(true);
    }
    
    // Coroutine to display feedback and advance the question
    private IEnumerator ShowFeedbackAndAdvance()
    {
        yield return new WaitForSeconds(feedbackDisplayTime);

        // Hide feedback
        feedbackTextObject.SetActive(false);

        // Check if we finished the quiz
        if (currentQuestionIndex < allQuestions.Length)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            // Quiz finished
            questionText.text = "CONGRATULATIONS! LEVEL COMPLETE!";
            Debug.Log("Quiz finished! Next level logic goes here.");
            DisableButtons(); // Permanently disable buttons on completion
        }
    }
    
    // Coroutine to display feedback and re-enable buttons after a short delay
    private IEnumerator ShowFeedbackAndReEnable()
    {
        yield return new WaitForSeconds(feedbackDisplayTime);

        // Hide feedback
        feedbackTextObject.SetActive(false);
        
        // Re-enable buttons for the user to try again
        EnableButtons();
    }


    private void DisplayQuestion(int index)
    {
        // Safety check is handled in ShowFeedbackAndAdvance()
        QuizQuestionData currentQ = allQuestions[index];
        
        // 1. Update the question text
        questionText.text = currentQ.QuestionText;
        
        // 2. Clear result message and ensure buttons are interactable
        feedbackTextObject.SetActive(false);
        EnableButtons(); 

        // 3. Hide all buttons, then only show and update the ones needed
        foreach (var button in allAnswerButtons)
        {
            if (button != null)
            {
                button.gameObject.SetActive(false);
            }
        }
        
        // Check if the data is consistent with the available UI
        if (currentQ.AnswerChoices.Length > allAnswerButtons.Count)
        {
            Debug.LogError($"Question {index} has {currentQ.AnswerChoices.Length} choices, but only {allAnswerButtons.Count} buttons are available!");
            return;
        }

        // Display the choice text on the corresponding buttons
        for (int i = 0; i < currentQ.AnswerChoices.Length; i++)
        {
            Button currentButton = allAnswerButtons[i];
            currentButton.gameObject.SetActive(true);

            // Find the TextMeshProUGUI component that is a child of the button
            TextMeshProUGUI buttonText = currentButton.GetComponentInChildren<TextMeshProUGUI>();
            
            if (buttonText != null)
            {
                // Set the text of the button to the choice string
                buttonText.text = currentQ.AnswerChoices[i];
            }
            else
            {
                Debug.LogError($"Button {currentButton.name} is missing a TextMeshProUGUI child component! Please fix the UI setup.");
            }
        }
    }

    // --- Utility Methods ---

    private void DisableButtons()
    {
        foreach (var button in allAnswerButtons)
        {
            if (button != null && button.gameObject.activeInHierarchy)
            {
                button.interactable = false;
            }
        }
    }
    
    private void EnableButtons()
    {
        foreach (var button in allAnswerButtons)
        {
            if (button != null && button.gameObject.activeInHierarchy)
            {
                button.interactable = true;
            }
        }
    }
}