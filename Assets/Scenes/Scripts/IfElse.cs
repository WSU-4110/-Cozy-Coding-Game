using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IfElseGame : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI WolfText;
    public TextMeshProUGUI CodeText;
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI FeedbackText;

    public List<Button> OptionButtons;
    public List<TextMeshProUGUI> OptionLabels;

    public Button NextButton;

    private int stage = 0;
    private bool waitingForChoice = false;

    void Start()
    {
        NextButton.onClick.AddListener(NextStage);
        DisableOptions();
        RunStage();
    }

    void NextStage()
    {
        if (waitingForChoice) return;

        stage++;
        RunStage();
    }

    void RunStage()
    {
        FeedbackText.text = "";
        DisableOptions();

        switch (stage)
        {
            case 0:
                Intro();
                break;

            // IF
            case 1:
                TeachIf();
                break;
            case 2:
                IfMiniGame();
                break;

            // ELSE
            case 3:
                TeachElse();
                break;
            case 4:
                ElseMiniGame();
                break;

            // ELIF
            case 5:
                TeachElif();
                break;
            case 6:
                ElifMiniGame();
                break;

            // Short-hand IF
            case 7:
                TeachShorthand();
                break;

            // Logical Operators
            case 8:
                TeachLogical();
                break;
            case 9:
                LogicalMiniGame();
                break;

            // Nested If
            case 10:
                TeachNestedIf();
                break;

            case 11:
                Ending();
                break;

            default:
                WolfText.text = "End of lesson!";
                CodeText.text = "";
                QuestionText.text = "";
                FeedbackText.text = "";
                break;
        }
    }

    // --------------------- STAGES ---------------------

    void Intro()
    {
        WolfText.text = "Welcome to the Path of Decisions!";
        CodeText.text = "";
        QuestionText.text = "Let's learn how IF and ELSE guide your code.";
    }

    void TeachIf()
    {
        WolfText.text = "In Python, IF checks a condition. If true, the code runs!";
        CodeText.text =
            "x = 5\n" +
            "if x > 3:\n" +
            "    print(\"x is big!\")";
        QuestionText.text = "Press NEXT when ready.";
    }

    void IfMiniGame()
    {
        WolfText.text = "Mini-game #1: Which IF statement prints 'OK'?";
        CodeText.text =
            "temperature = 20\n" +
            "if _____:\n" +
            "    print(\"OK\")";

        QuestionText.text = "Choose the correct condition:";
        SetupChoices(
            new string[] {
                "temperature < 10",
                "temperature == 20",
                "temperature > 50"
            },
            correctIndex: 1
        );
    }

    void TeachElse()
    {
        WolfText.text = "ELSE runs when the IF condition is false.";
        CodeText.text =
            "if score >= 50:\n" +
            "    print(\"Pass\")\n" +
            "else:\n" +
            "    print(\"Fail\")";
        QuestionText.text = "Press NEXT to continue.";
    }

    void ElseMiniGame()
    {
        WolfText.text = "Mini-game #2: What does this print?";
        CodeText.text =
        "score = 30\n" +
        "if score >= 50:\n" +
        "    print(\"Pass\")\n" +
        "else:\n" +
        "    print(\"Fail\")";

        SetupChoices(
            new string[] { "Pass", "Fail", "Error" },
            correctIndex: 1
        );
    }

    void TeachElif()
    {
        WolfText.text = "ELIF lets you check multiple conditions in order!";
        CodeText.text =
            "if x > 10:\n" +
            "    print(\"Big\")\n" +
            "elif x > 5:\n" +
            "    print(\"Medium\")\n" +
            "else:\n" +
            "    print(\"Small\")";
        QuestionText.text = "Press NEXT when ready.";
    }

    void ElifMiniGame()
    {
        WolfText.text = "Mini-game #3: Predict the output!";
        CodeText.text =
            "x = 7\n" +
            "if x > 10:\n" +
            "    print(\"Big\")\n" +
            "elif x > 5:\n" +
            "    print(\"Medium\")\n" +
            "else:\n" +
            "    print(\"Small\")";

        SetupChoices(
            new string[] { "Big", "Medium", "Small" },
            correctIndex: 1
        );
    }

    void TeachShorthand()
    {
        WolfText.text = "Shorthand IF lets you write small checks in one line!";
        CodeText.text =
            "print(\"Yes\") if a == 5 else print(\"No\")";
        QuestionText.text = "Press NEXT.";
    }

    void TeachLogical()
    {
        WolfText.text = "Logical operators allow complex conditions!";
        CodeText.text =
            "if age > 18 and age < 30:\n" +
            "    print(\"Young adult\")\n\n" +
            "if temperature < 0 or raining:\n" +
            "    print(\"Stay inside\")";
        QuestionText.text = "Let's try a puzzle!";
    }

    void LogicalMiniGame()
    {
        WolfText.text = "Logic Puzzle: When is 'SAFE' printed?";
        CodeText.text =
            "x = 4\n" +
            "y = 10\n" +
            "if x < 5 and y < 20:\n" +
            "    print(\"SAFE\")";

        SetupChoices(
            new string[] {
                "When x < 5 AND y < 20",
                "When x > 5 OR y > 20",
                "Never"
            },
            correctIndex: 0
        );
    }

    void TeachNestedIf()
    {
        WolfText.text = "Nested IF statements allow deeper checking!";
        CodeText.text =
            "x = 10\n" +
            "if x > 5:\n" +
            "    if x < 20:\n" +
            "        print(\"In range!\")";
        QuestionText.text = "Press NEXT to finish.";
    }

    void Ending()
    {
        WolfText.text = "Great job! You've mastered IF, ELSE, ELIF, logic, and nested conditions!";
        CodeText.text = "";
        QuestionText.text = "You may leave the forest of decisions!";
        DisableOptions();
    }

    // --------------------- CHOICES ---------------------

    void SetupChoices(string[] labels, int correctIndex)
    {
        waitingForChoice = true;

        for (int i = 0; i < OptionButtons.Count; i++)
        {
            OptionButtons[i].gameObject.SetActive(i < labels.Length);
            OptionLabels[i].text = labels[i];

            int choice = i;
            OptionButtons[i].onClick.RemoveAllListeners();
            OptionButtons[i].onClick.AddListener(() => Choose(choice == correctIndex));
        }
    }

    void Choose(bool correct)
    {
        waitingForChoice = false;

        if (correct)
            FeedbackText.text = "<color=#6aff7f>Correct!</color>";
        else
            FeedbackText.text = "<color=#ff6a6a>Incorrect!</color>";

        DisableOptions();
    }

    void DisableOptions()
    {
        foreach (var b in OptionButtons)
            b.gameObject.SetActive(false);
    }
}
