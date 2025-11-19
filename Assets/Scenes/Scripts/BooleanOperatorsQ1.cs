using UnityEngine;
using TMPro;

public class BooleanOperatorsQ1 : MonoBehaviour
{
    public TMP_Text feedbackText;
    public TMP_Text questionText;

    private string[] questions = {
        "What would the following statement print if ran?\nprint(10>9)",
        "Operators can't be used with booleans.",
        "What would print(bool('abc')) print?"
    };

    private bool[] answers = { true, false, true};
    private int currentQuestion = 0;

    private void Start()
    {
        questionText.text = questions[currentQuestion];
        feedbackText.text = "";
    }

    public void Answer(bool playerAnswer)
    {
        if (playerAnswer == answers[currentQuestion])
            feedbackText.text = "That's correct!";
        else
            feedbackText.text = "That's wrong, try again!";
    }

    public void NextQuestion()
    {
        currentQuestion++;
        if (currentQuestion >= questions.Length)
            currentQuestion = 0;

        questionText.text = questions[currentQuestion];
        feedbackText.text = "";
    }
}

