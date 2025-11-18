using UnityEngine;
using UnityEngine.UI;
using TMPro; // Needed for TextMeshPro
using System.Collections.Generic;
using System.Collections; 

/// <summary>
/// Manages the quiz flow for the CastingLevel scene.
/// Handles question display, answer checking, and feedback.
/// Attach this to the 'QuizManager' GameObject.
/// </summary>
public class CastingQuizManager : MonoBehaviour
{
    // --- Quiz Data Structure ---
    [Header("Quiz Data")]
    // NOW USING THE NEW, DEDICATED DATA STRUCTURE (CastingQuestionData.cs)
    public CastingQuestionData[] allQuestions = new CastingQuestionData[]
    {
        // Question 1: int() casting (Correct answer: 35)
        new CastingQuestionData 
        {
            QuestionText = "What will be the result of the following code:\nprint(int(35.88))",
            AnswerChoices = new string[] { "35", "35.88", "36" },
            CorrectAnswer = "35"
        },
        // Question 2: float() casting (Correct answer: 35.0)
        new CastingQuestionData 
        {
            QuestionText = "What will be the result of the following code:\nprint(float(35))",
            AnswerChoices = new string[] { "35", "35.0", "0.35" },
            CorrectAnswer = "35.0"
        },
        // Question 3: str() casting (Correct answer: 35.82)
        new CastingQuestionData 
        {
            QuestionText = "What will be the result of the following code:\nprint(str(35.82))",
            AnswerChoices = new string[] { "35", "35.8", "35.82" },
            CorrectAnswer = "35.82"
        }
    };
    private int currentQuestionIndex = 0;

    // --- References to UI Components ---
    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText; 
    public Button answer1Button;
    public Button answer2Button;
    public Button answer3Button;

    private List<Button> allAnswerButtons;

    // --- Configuration ---
    [Header("Configuration")]
    public float feedbackDisplayTime = 1.5f; 

    void Start()
    {
        // Initialize the list of buttons
        allAnswerButtons = new List<Button>();
        if (answer1Button != null) allAnswerButtons.Add(answer1Button);
        if (answer2Button != null) allAnswerButtons.Add(answer2Button);
        if (answer3Button != null) allAnswerButtons.Add(answer3Button);

        // Hide feedback text at the start
        feedbackText.gameObject.SetActive(false);

        // Assign the click function to each button dynamically.
        // This ensures the correct answer text is passed to CheckAnswer().
        foreach (var button in allAnswerButtons)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    CheckAnswer(buttonText.text);
                }
            });
        }
        
        // Start the quiz
        if (allQuestions.Length > 0) 
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            Debug.LogError("No questions defined in the Casting Quiz Manager!");
        }
    }

    // --- Main Logic Methods ---

    public void CheckAnswer(string selectedAnswer)
    {
        DisableButtons(); 
        
        if (currentQuestionIndex >= allQuestions.Length) return;

        // Note: The type here is CastingQuestionData
        CastingQuestionData currentQ = allQuestions[currentQuestionIndex];
        string correctAnswer = currentQ.CorrectAnswer;

        // Check if the clicked answer matches the correct answer
        if (selectedAnswer == correctAnswer)
        {
            feedbackText.text = "CORRECT!";
            feedbackText.color = Color.green;
            
            currentQuestionIndex++;
            StartCoroutine(ShowFeedbackAndAdvance());
        }
        else
        {
            feedbackText.text = "INCORRECT. Remember, int() chops off the decimal, and str() converts anything to text!";
            feedbackText.color = Color.red; 
            
            StartCoroutine(ShowFeedbackAndReEnable());
        }

        feedbackText.gameObject.SetActive(true);
    }
    
    // Coroutine to display feedback and advance the question
    private IEnumerator ShowFeedbackAndAdvance()
    {
        yield return new WaitForSeconds(feedbackDisplayTime);

        feedbackText.gameObject.SetActive(false);

        // Check if we finished the quiz
        if (currentQuestionIndex < allQuestions.Length)
        {
            DisplayQuestion(currentQuestionIndex);
        }
        else
        {
            // Quiz finished - NEXT LEVEL LOGIC HERE
            questionText.text = "LEVEL COMPLETE! You're a casting expert!";
            Debug.Log("Casting Quiz finished! Next level logic goes here.");
            DisableButtons(); 
        }
    }
    
    // Coroutine to display feedback and re-enable buttons after a short delay
    private IEnumerator ShowFeedbackAndReEnable()
    {
        yield return new WaitForSeconds(feedbackDisplayTime);

        feedbackText.gameObject.SetActive(false);
        EnableButtons();
    }


    private void DisplayQuestion(int index)
    {
        if (index >= allQuestions.Length) return;

        // Note: The type here is CastingQuestionData
        CastingQuestionData currentQ = allQuestions[index];
        
        // 1. Update the question text
        questionText.text = currentQ.QuestionText;
        
        // 2. Clear feedback and ensure buttons are interactable
        feedbackText.gameObject.SetActive(false);
        EnableButtons(); 

        // 3. Update all buttons with the new choice text
        if (currentQ.AnswerChoices.Length > allAnswerButtons.Count)
        {
            Debug.LogError($"Question {index} has too many choices for the available buttons!");
            return;
        }

        for (int i = 0; i < allAnswerButtons.Count; i++)
        {
            Button currentButton = allAnswerButtons[i];
            
            // Show only the buttons needed for this question
            if (i < currentQ.AnswerChoices.Length)
            {
                currentButton.gameObject.SetActive(true);
                TextMeshProUGUI buttonText = currentButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = currentQ.AnswerChoices[i];
                }
            }
            else
            {
                currentButton.gameObject.SetActive(false); // Hide unused buttons
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