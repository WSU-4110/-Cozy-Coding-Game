using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalQuizGame : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI WolfText;
    public TextMeshProUGUI FeedbackText;
    public TextMeshProUGUI CounterText;
    public TextMeshProUGUI ScorePreviewText;

    public List<Button> OptionButtons;
    public List<TextMeshProUGUI> OptionLabels;

    public TMP_InputField AnswerInput;
    public Button SubmitButton;
    public Button NextButton;

    private string[] questions;
    private string[,] options;      
    private int[] correctOption;    // -1 means "use freeTextAnswer instead"
    private string[] freeTextAnswer;

    private int currentIndex = 0;
    private int score = 0;
    private int totalQuestions = 20;
    private bool waitingForAnswer = false;

    void Start()
    {
        BuildQuestionData();

        for (int i = 0; i < OptionButtons.Count; i++)
        {
            int choice = i;
            OptionButtons[i].onClick.RemoveAllListeners();
            OptionButtons[i].onClick.AddListener(() => ChooseOption(choice));
        }

        SubmitButton.onClick.RemoveAllListeners();
        SubmitButton.onClick.AddListener(SubmitFreeAnswer);

        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(NextQuestion);

        currentIndex = 0;
        score = 0;
        ShowQuestion();
    }

    void BuildQuestionData()
    {
        // 20 questions
        questions = new string[20];
        options = new string[20, 4];
        correctOption = new int[20];
        freeTextAnswer = new string[20];

        int q = 0;

        // 0 – Strings basics (multiple choice)
        questions[q] = "1) In Python, what type of value is \"Hello, class!\" ?";
        options[q, 0] = "An integer (int)";
        options[q, 1] = "A string (str)";
        options[q, 2] = "A list";
        options[q, 3] = "A tuple";
        correctOption[q] = 1;

        // 1 – Indexing strings
        q++;
        questions[q] = "2) x = \"wolf\"  •  What does x[0] give you?";
        options[q, 0] = "\"w\"";
        options[q, 1] = "\"o\"";
        options[q, 2] = "\"f\"";
        options[q, 3] = "An error";
        correctOption[q] = 0;

        // 2 – If / else
        q++;
        questions[q] =
            "3) What does this print?\n\n" +
            "age = 18\n" +
            "if age >= 18:\n" +
            "    print(\"Adult student\")\n" +
            "else:\n" +
            "    print(\"Young student\")";
        options[q, 0] = "Adult student";
        options[q, 1] = "Young student";
        options[q, 2] = "Nothing";
        options[q, 3] = "Error";
        correctOption[q] = 0;

        // 3 – Comments
        q++;
        questions[q] = "4) Which line is a Python comment?";
        options[q, 0] = "print(\"Homework done\")";
        options[q, 1] = "# Remember to save your file";
        options[q, 2] = "\"This is a comment\"";
        options[q, 3] = "comment: this is code";
        correctOption[q] = 1;

        // 4 – Data types
        q++;
        questions[q] = "5) Which is a float (decimal number) in Python?";
        options[q, 0] = "7";
        options[q, 1] = "\"7\"";
        options[q, 2] = "7.0";
        options[q, 3] = "True";
        correctOption[q] = 2;

        // 5 – Casting
        q++;
        questions[q] = "6) What does int(\"5\") do?";
        options[q, 0] = "Turns 5 into text";
        options[q, 1] = "Turns the string \"5\" into the number 5";
        options[q, 2] = "Gives an error";
        options[q, 3] = "Creates a list";
        correctOption[q] = 1;

        // 6 – Global variables
        q++;
        questions[q] =
            "7) Inside a function, what does the keyword global score mean?";
        options[q, 0] = "Make a new local variable called score";
        options[q, 1] = "Use the score variable from outside the function";
        options[q, 2] = "Delete the score variable";
        options[q, 3] = "Turn score into a string";
        correctOption[q] = 1;

        // 7 – Lists
        q++;
        questions[q] =
            "8) students = [\"Mia\", \"Leo\", \"Sam\"]\n" +
            "What is students[1]?";
        options[q, 0] = "\"Mia\"";
        options[q, 1] = "\"Leo\"";
        options[q, 2] = "\"Sam\"";
        options[q, 3] = "It gives an error";
        correctOption[q] = 1;

        // 8 – Tuples
        q++;
        questions[q] = "9) How is a tuple different from a list?";
        options[q, 0] = "Tuples are mutable, lists are not";
        options[q, 1] = "Lists are immutable, tuples are not";
        options[q, 2] = "Tuples are immutable (cannot change)";
        options[q, 3] = "They are exactly the same";
        correctOption[q] = 2;

        // 9 – Multiple assignment
        q++;
        questions[q] = "10) What does x, y = 3, 5 do?";
        options[q, 0] = "Nothing, it is invalid syntax";
        options[q, 1] = "Sets x to 3 and y to 5";
        options[q, 2] = "Creates a list [3, 5]";
        options[q, 3] = "Creates a tuple (3, 5) and ignores it";
        correctOption[q] = 1;

        // 10 – Numbers & arithmetic
        q++;
        questions[q] = "11) What is the result of 7 // 2 in Python?";
        options[q, 0] = "3.5";
        options[q, 1] = "3";
        options[q, 2] = "4";
        options[q, 3] = "Error";
        correctOption[q] = 1;

        // 11 – Output / print
        q++;
        questions[q] =
            "12) Which line prints both a name and a score nicely?";
        options[q, 0] = "print(\"Score: \" + name + score)";
        options[q, 1] = "print(\"Score:\", name, score)";
        options[q, 2] = "print(name score)";
        options[q, 3] = "print(name: score)";
        correctOption[q] = 1;

        // 12 – Syntax error spotter
        q++;
        questions[q] =
            "13) Which line has correct syntax for an if statement?";
        options[q, 0] = "if x > 0";
        options[q, 1] = "if x > 0:";
        options[q, 2] = "if (x > 0)";
        options[q, 3] = "if x > 0 then";
        correctOption[q] = 1;

        // 13 – Casting to string
        q++;
        questions[q] =
            "14) score = 10\n" +
            "Which line safely makes score part of a sentence?";
        options[q, 0] = "print(\"Score: \" + score)";
        options[q, 1] = "print(\"Score: \" + str(score))";
        options[q, 2] = "print(\"Score: \" score)";
        options[q, 3] = "print(str + score)";
        correctOption[q] = 1;

        // 14 – Free-response: length of string
        q++;
        questions[q] =
            "15) x = \"Hello class\"  •  What does len(x) return?\n" +
            "(Type just the number.)";
        correctOption[q] = -1;        // use freeTextAnswer
        freeTextAnswer[q] = "11";     // H e l l o _ c l a s s  (5 + 1 + 5)

        // 15 – Free-response: in keyword
        q++;
        questions[q] =
            "16) txt = \"Python is fun\"  •  Fill in the blank:\n" +
            "if \"fun\" ____ txt:\n" +
            "    print(\"Yes!\")\n\n(Type the missing keyword.)";
        correctOption[q] = -1;
        freeTextAnswer[q] = "in";

        // 16 – Free-response: list index
        q++;
        questions[q] =
            "17) nums = [10, 20, 30]  •  What is nums[2] ?\n" +
            "(Type just the number.)";
        correctOption[q] = -1;
        freeTextAnswer[q] = "30";

        // 17 – Free-response: boolean from comparison
        q++;
        questions[q] =
            "18) What does this print?\n\n" +
            "x = 5\n" +
            "print(x > 2)\n\n(Type True or False.)";
        correctOption[q] = -1;
        freeTextAnswer[q] = "true";

        // 18 – Free-response: tuple
        q++;
        questions[q] =
            "19) Write the Python syntax for a tuple storing (2, 4, 6)\n" +
            "(Type exactly how it would look in code.)";
        correctOption[q] = -1;
        freeTextAnswer[q] = "(2, 4, 6)";

        // 19 – Final multiple choice recap
        q++;
        questions[q] =
            "20) Which sentence is TRUE about what you learned?\n";
        options[q, 0] = "You can never change a list.";
        options[q, 1] = "Strings, lists, and tuples can all be indexed with [ ]";
        options[q, 2] = "if / else is only for numbers, not strings.";
        options[q, 3] = "global makes a brand-new variable every time.";
        correctOption[q] = 1;
    }

    void ShowQuestion()
    {
        if (currentIndex >= totalQuestions)
        {
            ShowFinalResult();
            return;
        }

        FeedbackText.text = "";

        QuestionText.text = questions[currentIndex];

        CounterText.text = "Question " + (currentIndex + 1) + " / " + totalQuestions;
        ScorePreviewText.text = "Score: " + score + " / " + totalQuestions;

        int correct = correctOption[currentIndex];

        if (correct >= 0)
        {
            // multiple choice
            AnswerInput.gameObject.SetActive(false);
            SubmitButton.gameObject.SetActive(false);

            for (int i = 0; i < OptionButtons.Count; i++)
            {
                string label = options[currentIndex, i];

                if (string.IsNullOrEmpty(label))
                {
                    OptionButtons[i].gameObject.SetActive(false);
                }
                else
                {
                    OptionButtons[i].gameObject.SetActive(true);
                    OptionLabels[i].text = label;
                }
            }
        }
        else
        {
            // free text answer
            for (int i = 0; i < OptionButtons.Count; i++)
            {
                OptionButtons[i].gameObject.SetActive(false);
            }

            AnswerInput.text = "";
            AnswerInput.gameObject.SetActive(true);
            SubmitButton.gameObject.SetActive(true);
        }

        // Let player answer; hide next until after
        NextButton.gameObject.SetActive(false);
        waitingForAnswer = true;

        WolfText.text = "Professor Wolf: Take your time, think it through!";
    }

    void ChooseOption(int index)
    {
        if (!waitingForAnswer) return;

        int correct = correctOption[currentIndex];

        if (index == correct)
        {
            FeedbackText.text = "<color=#6aff7f>Correct! Nice job.</color>";
            score++;
        }
        else
        {
            FeedbackText.text = "<color=#ff6a6a>Good try! Look back at the lesson notes.</color>";
        }

        waitingForAnswer = false;
        NextButton.gameObject.SetActive(true);
    }

    void SubmitFreeAnswer()
    {
        if (!waitingForAnswer) return;

        string expected = freeTextAnswer[currentIndex];
        string user = AnswerInput.text.Trim();

        // compare lowercase & remove spaces for forgiving check
        string cleanExpected = expected.ToLower().Replace(" ", "");
        string cleanUser = user.ToLower().Replace(" ", "");

        if (cleanUser == cleanExpected)
        {
            FeedbackText.text = "<color=#6aff7f>Correct! You remembered it!</color>";
            score++;
        }
        else
        {
            FeedbackText.text = "<color=#ff6a6a>Nice attempt! The answer was: " +
                                expected + "</color>";
        }

        waitingForAnswer = false;
        NextButton.gameObject.SetActive(true);
    }

    void NextQuestion()
    {
        if (waitingForAnswer) return;

        currentIndex++;
        ShowQuestion();
    }

    void ShowFinalResult()
    {
        // hide answer UI
        for (int i = 0; i < OptionButtons.Count; i++)
            OptionButtons[i].gameObject.SetActive(false);

        AnswerInput.gameObject.SetActive(false);
        SubmitButton.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(false);

        // letter grade
        float percent = (float)score / totalQuestions;
        string grade;

        if (percent >= 0.9f) grade = "A";
        else if (percent >= 0.8f) grade = "B";
        else if (percent >= 0.7f) grade = "C";
        else if (percent >= 0.6f) grade = "D";
        else grade = "F";

        QuestionText.text =
            "Final Quiz Complete!\n\n" +
            "You scored " + score + " out of " + totalQuestions + ".\n" +
            "Letter grade: " + grade;

        ScorePreviewText.text = "";

        if (grade == "A" || grade == "B")
        {
            WolfText.text =
                "Professor Wolf: Amazing work! You’ve really grown as a coder.\n" +
                "Keep practicing and you’ll be building your own games in no time!";
        }
        else
        {
            WolfText.text =
                "Professor Wolf: Hey, you still did great for trying.\n" +
                "Every programmer learns at their own pace. Review a few topics,\n" +
                "try again later, and I know you’ll improve!";
        }

        FeedbackText.text = "";
        CounterText.text = "";
    }
}
