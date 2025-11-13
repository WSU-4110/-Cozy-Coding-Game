using UnityEngine;

// The System.Serializable attribute is essential for Unity to display this data in the Inspector!
[System.Serializable]
public class QuizQuestionData
{
    // The main question text to display
    public string QuestionText;

    // The answer choice text displayed in the bubbles (e.g., "int", "float", "long")
    public string[] AnswerChoices;

    // The expected correct answer string (must match one of the AnswerChoices)
    public string CorrectAnswer;

    // The index of the correct answer in the AnswerChoices array (Optional, but useful)
    public int CorrectAnswerIndex;
}