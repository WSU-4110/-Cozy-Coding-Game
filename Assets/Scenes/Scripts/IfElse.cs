using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IfElse : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI WolfText;
    public TextMeshProUGUI CodeText;
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI FeedbackText;

    public List<Button> OptionButtons;
    public List<TextMeshProUGUI> OptionLabels;

    public Button NextButton;

    // Tracks which part of the lesson we are in
    [SerializeField, HideInInspector]
    private int stage = 0;

    private bool waitingForChoice = false;

    void Start()
    {
        // ensure lesson starts at stage 0
        stage = 0;
        waitingForChoice = false;

        // Hook up NEXT button
        NextButton.onClick.RemoveAllListeners();
        NextButton.onClick.AddListener(NextStage);

        DisableOptions();
        RunStage();
    }

    void NextStage()
    {
        if (waitingForChoice)
            return;

        stage++;
        RunStage();
    }

    void RunStage()
    {
        FeedbackText.text = "";
        DisableOptions();

        switch (stage)
        {
            case 0: Intro(); break;

            case 1: TeachIf(); break;
            case 2: IfMiniGame(); break;

            case 3: TeachElse(); break;
            case 4: ElseMiniGame(); break;

            case 5: TeachElif(); break;
            case 6: ElifMiniGame(); break;

            case 7: TeachShorthand(); break;

            case 8: TeachLogical(); break;
            case 9: LogicalMiniGame(); break;

            case 10: TeachNestedIf(); break;

            case 11: Ending(); break;

            default:
                WolfText.text = "End of lesson!";
                CodeText.text = "";
                QuestionText.text = "";
                FeedbackText.text = "";
                break;
        }
    }

    // ---------------- LESSON STAGES ----------------

    void Intro()
    {
        WolfText.text = "Welcome to the Path of Decisions!";
        CodeText.text = "";
        QuestionText.text = "Let's learn how IF and ELSE guide your code.";
    }

    void TeachIf()
    {
        WolfText.text = "IF checks if a condition is true.";
        CodeText.text =
            "x = 5\n" +
            "if x > 3:\n" +
            "    print(\"x is big!\")";
        QuestionText.text = "Press NEXT to continue.";
    }

    void IfMiniGame()
    {
        WolfText.text = "Mini-game #1: Which IF condition prints 'OK'?";
        CodeText.text =
            "temperature = 20\n" +
            "if _____:\n" +
            "    print(\"OK\")";

        QuestionText.text = "Choose the correct condition:";

        SetupChoices(
            new string[]
            {
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
        WolfText.text = "ELIF lets you check multiple conditions!";
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
        WolfText.text = "Shorthand IF lets you write quick checks.";
        CodeText.text = "print(\"Yes\") if a == 5 else print(\"No\")";
        QuestionText.text = "Press NEXT.";
    }

    void TeachLogical()
    {
        WolfText.text = "Logical operators allow multiple conditions!";
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
            new string[]
            {
                "When x < 5 AND y < 20",
                "When x > 5 OR y > 20",
                "Never"
            },
            correctIndex: 0
        );
    }

    void TeachNestedIf()
    {
        WolfText.text = "Nested IF checks inside another IF!";
        CodeText.text =
            "x = 10\n" +
            "if x > 5:\n" +
            "    if x < 20:\n" +
            "        print(\"In range!\")";
        QuestionText.text = "Press NEXT to finish.";
    }

    void Ending()
    {
        WolfText.text = "Great job! You've mastered conditions!";
        CodeText.text = "";
        QuestionText.text = "You may leave the forest of decisions.";
        DisableOptions();
    }

    // ---------------- CHOICE SYSTEM ----------------

    void SetupChoices(string[] labels, int correctIndex)
    {
        waitingForChoice = true;

        for (int i = 0; i < OptionButtons.Count; i++)
        {
            bool shouldShow = i < labels.Length;
            OptionButtons[i].gameObject.SetActive(shouldShow);

            if (!shouldShow) continue;

            OptionLabels[i].text = labels[i];

            int choice = i;
            OptionButtons[i].onClick.RemoveAllListeners();
            OptionButtons[i].onClick.AddListener(() =>
            {
                Choose(choice == correctIndex, correctIndex);
            });
        }
    }

    void Choose(bool correct, int correctIndex)
    {
        if (correct)
        {
            FeedbackText.text = "<color=#6aff7f>Correct!</color>";
            waitingForChoice = false;
            DisableOptions();     // hide buttons
        }
        else
        {
            FeedbackText.text = "<color=#ff6a6a>Incorrect! Try again.</color>";

            // Re-enable ALL options so player can retry
            for (int i = 0; i < OptionButtons.Count; i++)
            {
                OptionButtons[i].gameObject.SetActive(i < OptionLabels.Count);
            }

            // Do NOT allow NEXT, do NOT advance stage  
            waitingForChoice = true;
        }
    }


    void DisableOptions()
    {
        foreach (var b in OptionButtons)
            b.gameObject.SetActive(false);
    }
}
