﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VariableNamesLevelCascading : MonoBehaviour
{
    [Header("Dialogue UI")]
    public TextMeshProUGUI speechvar;
    public Button continueButton;

    [Header("Mini-Game UI")]
    public TextMeshProUGUI badName1;
    public TMP_InputField input1;
    public Button enter1;

    public TextMeshProUGUI badName2;
    public TMP_InputField input2;
    public Button enter2;

    public TextMeshProUGUI badName3;
    public TMP_InputField input3;
    public Button enter3;

    public TextMeshProUGUI feedbackText;
    public Button nextLevelButton;

    private int dialogueIndex = 0;
    private bool gameStarted = false;

    private string[] dialogueLines = {
        "Naming variables can be tricky.",
        "They should be simple but descriptive.",
        "They shouldn’t start with numbers or include spaces.",
        "Fix these variable names!"
    };

    private string[] badNames = {
        "2player score",
        "player health",
        "level-name"
    };

    private string[] explanations = {
        "Variable names can’t start with a number.",
        "No spaces—try camelCase like playerHealth.",
        "Hyphens aren’t allowed; use underscores or camelCase."
    };

    private int currentIndex = 0;

    void Start()
    {
        speechvar.text = dialogueLines[0];
        feedbackText.text = "";
        nextLevelButton.gameObject.SetActive(false);

        // Hide all mini-game elements at first
        SetMiniGameActive(false);

        // Hook buttons
        continueButton.onClick.AddListener(NextDialogue);
        enter1.onClick.AddListener(() => CheckAnswer(1));
        enter2.onClick.AddListener(() => CheckAnswer(2));
        enter3.onClick.AddListener(() => CheckAnswer(3));
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueLines.Length)
        {
            speechvar.text = dialogueLines[dialogueIndex];
        }
        else
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        continueButton.gameObject.SetActive(false);

        // Show first question
        badName1.gameObject.SetActive(true);
        input1.gameObject.SetActive(true);
        enter1.gameObject.SetActive(true);
        badName1.text = badNames[0];
        speechvar.text = "Fix these variable names!";
    }

    void CheckAnswer(int index)
    {
        if (!gameStarted) return;

        TMP_InputField input = null;
        TextMeshProUGUI badName = null;
        Button enterButton = null;
        string explanation = "";

        switch (index)
        {
            case 1:
                input = input1;
                badName = badName1;
                enterButton = enter1;
                explanation = explanations[0];
                break;
            case 2:
                input = input2;
                badName = badName2;
                enterButton = enter2;
                explanation = explanations[1];
                break;
            case 3:
                input = input3;
                badName = badName3;
                enterButton = enter3;
                explanation = explanations[2];
                break;
        }

        string answer = input.text.Trim();

        if (IsValidVariableName(answer))
        {
            feedbackText.text = $"Correct! \"{answer}\" fixes \"{badName.text}\".";
            input.interactable = false;
            enterButton.gameObject.SetActive(false);

            // Show next question if any
            currentIndex++;
            ShowNextQuestion();
        }
        else
        {
            feedbackText.text = "Try again. " + explanation;
        }
    }

    void ShowNextQuestion()
    {
        if (currentIndex == 1)
        {
            badName2.gameObject.SetActive(true);
            input2.gameObject.SetActive(true);
            enter2.gameObject.SetActive(true);
            badName2.text = badNames[1];
        }
        else if (currentIndex == 2)
        {
            badName3.gameObject.SetActive(true);
            input3.gameObject.SetActive(true);
            enter3.gameObject.SetActive(true);
            badName3.text = badNames[2];
        }
        else if (currentIndex >= 3)
        {
            EndGame();
        }
    }

    bool IsValidVariableName(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        if (!char.IsLetter(name[0]) && name[0] != '_') return false;
        foreach (char c in name)
        {
            if (!char.IsLetterOrDigit(c) && c != '_') return false;
        }
        return true;
    }

    void EndGame()
    {
        gameStarted = false;
        input1.gameObject.SetActive(false);
        input2.gameObject.SetActive(false);
        input3.gameObject.SetActive(false);
        enter1.gameObject.SetActive(false);
        enter2.gameObject.SetActive(false);
        enter3.gameObject.SetActive(false);

        speechvar.text = "Awesome! You’ve mastered variable names!";
        feedbackText.text = "";
        nextLevelButton.gameObject.SetActive(true);
    }

    void SetMiniGameActive(bool active)
    {
        badName1.gameObject.SetActive(active);
        input1.gameObject.SetActive(active);
        enter1.gameObject.SetActive(active);

        badName2.gameObject.SetActive(false);
        input2.gameObject.SetActive(false);
        enter2.gameObject.SetActive(false);

        badName3.gameObject.SetActive(false);
        input3.gameObject.SetActive(false);
        enter3.gameObject.SetActive(false);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("NextSceneName"); // Replace with your next scene
    }
}
