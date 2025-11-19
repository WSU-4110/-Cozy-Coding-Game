using UnityEngine;
using TMPro;

public class GotIt : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI questionText;

    private int currentQuestion = 0;
    private bool answerIsCorrect = false;

    // Questions and answers
    private string[] questions = new string[]
    {
        "Write the code that prints the data type of x:",
        "The following code example would print the data type of x. What data type would that be?\n\nx = 5\nprint(type(x))",
        "The following code example would print the data type of x. What data type would that be?\n\nx = \"Hello World\"\nprint(type(x))"
    };

    private string[] answers = new string[]
    {
        "print(type(x))",
        "int",
        "str"
    };

    void Start()
    {
        // Start with question 1
        questionText.text = questions[currentQuestion];
        resultText.text = "";
        codeInputField.gameObject.SetActive(false);
    }

    public void OnGotItClick()
    {
        // If the current answer is correct, move to next question
        if (answerIsCorrect)
        {
            currentQuestion++;

            if (currentQuestion < questions.Length)
            {
                questionText.text = questions[currentQuestion];
                resultText.text = "";
                codeInputField.text = "";
                answerIsCorrect = false;
            }
            else
            {
                // End of quiz
                questionText.text = "🎉 Great job! You’ve completed all questions for Data Types! Now let's move on to Numbers.";
                resultText.text = "";
                codeInputField.gameObject.SetActive(false);
                return;
            }
        }

        // Show and focus input field for typing
        codeInputField.gameObject.SetActive(true);
        codeInputField.Select();
        codeInputField.ActivateInputField();
    }

    void Update()
    {
        // Check if Enter is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string userAnswer = codeInputField.text.Trim();

            if (userAnswer == answers[currentQuestion])
            {
                resultText.text = "✅ Correct!";
                resultText.color = Color.green;
                answerIsCorrect = true;
            }
            else
            {
                resultText.text = "❌ Wrong answer. Try again.";
                resultText.color = Color.red;
                answerIsCorrect = false;
            }
        }
    }
}
