using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SetQuiz : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    private int currentQuestion = 0;

    private string[] questions = {
        "Which one of these is a set?\nA) ('apple','banana','cherry')\nB) ['apple','banana','cherry']\nC) {'apple','banana','cherry'}",
        "Which one of these is a set?\nA) [1,2,3,4]\nB) (1,2,3,4)\nC) {1,2,3,4}",
        "Which one of these is a set?\nA) ['red','blue','green']\nB) ('red','blue','green')\nC) {'red','blue','green'}",
        "Which one is a set?\nA) {10,20,30}\nB) [10,20,30]\nC) '10,20,30'",
        "Which one is a set?\nA) ['a','b','c']\nB) ('a','b','c')\nC) {'a','b','c'}",
        "Which one is a set?\nA) ('pizza','taco','burger')\nB) ['pizza','taco','burger']\nC) {'pizza','taco','burger'}",
        "Which one is a set?\nA) [True,False]\nB) (True,False)\nC) {True,False}",
        "Which one is a set?\nA) ('A','B','C')\nB) ['A','B','C']\nC) {'A','B','C'}",
        "Which one is a set?\nA) ('dog','cat','fish')\nB) ['dog','cat','fish']\nC) {'dog','cat','fish'}",
        "Which one is a set?\nA) (9,8,7)\nB) [9,8,7]\nC) {9,8,7}"
    };

    private int[] correctAnswers = { 3, 3, 3, 1, 3, 3, 3, 3, 3, 3 };

    void Start()
    {
        LoadQuestion();
    }

    void LoadQuestion()
    {
        questionText.text = questions[currentQuestion];
        feedbackText.text = "";
    }

    public void ChooseA() { CheckAnswer(1); }
    public void ChooseB() { CheckAnswer(2); }
    public void ChooseC() { CheckAnswer(3); }

    void CheckAnswer(int choice)
    {
        if (choice == correctAnswers[currentQuestion])
        {
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Wrong! Try again.";
            return;
        }

        currentQuestion++;
        if (currentQuestion >= questions.Length)
        {
            feedbackText.text = "All questions complete!";
            Invoke("LoadMapScreen", 1.5f);
            return;
        }

        LoadQuestion();
    }

    void LoadMapScreen()
    {
        SceneManager.LoadScene("Map");
    }
}


