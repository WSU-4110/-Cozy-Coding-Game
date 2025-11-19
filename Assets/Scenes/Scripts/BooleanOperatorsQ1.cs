using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BooleanOperatorsQ1 : MonoBehaviour
{
    public TMP_Text feedbackText;
    public TMP_Text questionText;

    private string[] questions = {
        "1. What does print(10 > 9) output?",
        "2. Operators can't be used with booleans.",
        "3. What does print(bool('abc')) output?",
        "4. What does print(5 == 5) output?",
        "5. What does print(7 != 3) output?",
        "6. What does print(4 > 10) output?",
        "7. What does print(2 <= 2) output?",
        "8. What does print(False or False) output?",
        "9. What does print(True and False) output?",
        "10. What does print(3 < 1) output?"
    };

    private bool[] answers = { true, false, true, true, true, false, true, false, false, false };

    private int currentQuestion = 0;
    private bool canProceed = false;

    private void Start()
    {
        questionText.text = questions[currentQuestion];
        feedbackText.text = "";
        canProceed = false;
    }

    public void Answer(bool playerAnswer)
    {
        if (playerAnswer == answers[currentQuestion])
        {
            feedbackText.text = "That's correct!";
            canProceed = true;
        }
        else
        {
            feedbackText.text = "That's wrong, try again!";
            canProceed = false;
        }
    }

    public void NextQuestion()
    {
        if (!canProceed)
        {
            feedbackText.text = "You must get it right before continuing.";
            return;
        }

        currentQuestion++;

        if (currentQuestion >= questions.Length)
        {
            feedbackText.text = "All questions complete!";
            Invoke("LoadMapScreen", 1.5f);
            return;
        }

        questionText.text = questions[currentQuestion];
        feedbackText.text = "";
        canProceed = false;
    }

    private void LoadMapScreen()
    {
        SceneManager.LoadScene("Map");
    }
}



