using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ForestOfChoices : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scenarioText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI feedbackText;
    public Button ifButton;
    public Button elseButton;
    public Button nextButton;

    private int currentScenario = 0;

    private struct Scenario
    {
        public string condition;
        public string correctChoice; // "if" or "else"
        public string correctResult;
        public string wrongResult;
    }

    private Scenario[] scenarios = new Scenario[]
    {
        new Scenario {
            condition = "If it’s sunny, take the left path. Else, take the right.",
            correctChoice = "if",
            correctResult = "You followed the sunny path and found a field of flowers!",
            wrongResult = "You went the wrong way and got caught in the rain!"
        },
        new Scenario {
            condition = "If the number is greater than 10, choose IF. Else, choose ELSE.",
            correctChoice = "if",
            correctResult = "Correct! 15 > 10.",
            wrongResult = "Oops, that’s not greater than 10."
        },
        new Scenario {
            condition = "If you have an umbrella, choose IF. Else, choose ELSE.",
            correctChoice = "else",
            correctResult = "You wisely took the ELSE path and stayed dry!",
            wrongResult = "You forgot your umbrella!"
        }
    };

    void Start()
    {
        titleText.text = "Forest of Choices";
        LoadScenario();

        ifButton.onClick.AddListener(() => MakeChoice("if"));
        elseButton.onClick.AddListener(() => MakeChoice("else"));
        nextButton.onClick.AddListener(() => LoadScenario());
        nextButton.gameObject.SetActive(false);
    }

    void LoadScenario()
    {
        Scenario sc = scenarios[currentScenario];
        scenarioText.text = sc.condition;
        resultText.text = "";
        feedbackText.text = "";
        nextButton.gameObject.SetActive(false);
    }

    void MakeChoice(string choice)
    {
        Scenario sc = scenarios[currentScenario];

        if (choice == sc.correctChoice)
        {
            resultText.text = sc.correctResult;
            feedbackText.text = "Correct! That’s how ‘if’ logic works!";
        }
        else
        {
            resultText.text = sc.wrongResult;
            feedbackText.text = "That’s the ‘else’ path. Try again!";
        }

        nextButton.gameObject.SetActive(true);
        currentScenario = (currentScenario + 1) % scenarios.Length;
    }
}
