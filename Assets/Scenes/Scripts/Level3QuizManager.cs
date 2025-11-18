using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Level3QuizManager : MonoBehaviour
{
    // --- UI References ---
    public TextMeshProUGUI questionText;          // WolfDialogueText
    public TMP_InputField answerInputField;       // Your new input field
    public TextMeshProUGUI resultText;            // Your results display
    public Button submitButton;                   // GottButton

    // --- Quiz Data and State ---
    [System.Serializable]
    public class QuestionData
    {
        public string question;
        public string correctAnswer;
    }

    public List<QuestionData> questions = new List<QuestionData>();
    private int currentQuestionIndex = 0;
    private bool currentQuestionAnswered = false;

    void Start()
    {
        // 1. Set up the question data
        questions.Add(new QuestionData { 
            question = "If x = 5, what is a correct syntax for printing the data type of the variable x?", 
            correctAnswer = "print(type(x))" 
        });
        
        questions.Add(new QuestionData { 
            question = "The following code example would print the data type of x, what data type would that be? x = 5 print(type(x))", 
            correctAnswer = "int" 
        });
        
        questions.Add(new QuestionData { 
            question = "The following code example would print the data type of x, what data type would that be. x = \"Hello World\" print(type(x))", 
            correctAnswer = "str" 
        });

        // 2. Set up input field placeholder behavior
        answerInputField.onSelect.AddListener(delegate { OnInputFieldSelected(); });
        answerInputField.onDeselect.AddListener(delegate { OnInputFieldDeselected(); });

        // 3. Initial setup
        ShowCurrentQuestion();

        // 4. Set up the button listener
        submitButton.onClick.AddListener(CheckAnswer);
    }

    private void OnInputFieldSelected()
    {
        // Clear placeholder text when user clicks to type
        if (answerInputField.text == "Type your answer here...")
        {
            answerInputField.text = "";
        }
    }

    private void OnInputFieldDeselected()
    {
        // Restore placeholder if field is empty
        if (string.IsNullOrEmpty(answerInputField.text))
        {
            answerInputField.text = "Type your answer here...";
        }
    }

    private void CheckAnswer()
    {
        string userAnswer = answerInputField.text.Trim();
        
        // Don't check if it's the placeholder text
        if (userAnswer == "Type your answer here..." || string.IsNullOrEmpty(userAnswer))
        {
            resultText.text = "Please type an answer!";
            resultText.color = Color.yellow;
            return;
        }

        string correctAnswer = questions[currentQuestionIndex].correctAnswer;

        // Case-insensitive comparison, ignoring extra spaces
        if (userAnswer.ToLower() == correctAnswer.ToLower())
        {
            resultText.text = "Correct! ✓";
            resultText.color = Color.green;
            currentQuestionAnswered = true;
            
            // Move to next question after 1.5 seconds
            Invoke("MoveToNextQuestion", 1.5f);
        }
        else
        {
            resultText.text = "Incorrect. Try again!";
            resultText.color = Color.red;
            
            // Clear input field but stay on same question
            answerInputField.text = "";
            answerInputField.Select();
            answerInputField.ActivateInputField();
        }
    }

    private void MoveToNextQuestion()
    {
        currentQuestionIndex++;
        currentQuestionAnswered = false;
        
        if (currentQuestionIndex < questions.Count)
        {
            ShowCurrentQuestion();
        }
        else
        {
            // Quiz completed
            questionText.text = "Quiz Completed!";
            answerInputField.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);
            resultText.text = "Great job! You finished all questions.";
            resultText.color = Color.blue;
        }
    }

    private void ShowCurrentQuestion()
    {
        // Reset result text and input field
        resultText.text = "";
        answerInputField.text = "Type your answer here...";
        
        // Show current question
        questionText.text = questions[currentQuestionIndex].question;
        
        // Focus on input field
        answerInputField.Select();
        answerInputField.ActivateInputField();
    }
}