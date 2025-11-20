using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CaseGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI puzzleText;
    public TextMeshProUGUI feedbackText;

    // Buttons
    public Button upperButton;
    public Button lowerButton;
    public Button stripButton;

    
    private string originalPhrase = "tHe lOsT sCrIpT rEtUrNs...";
    private string currentPhrase;

    private string[] correctSequence = { "upper", "lower", "strip", "upper", "lower" };
    private int currentStep = 0;

    void Start()
    {
        ResetPuzzle();

        // Hook buttons
        upperButton.onClick.AddListener(() => HandlePress("upper"));
        lowerButton.onClick.AddListener(() => HandlePress("lower"));
        stripButton.onClick.AddListener(() => HandlePress("strip"));
    }

    void ResetPuzzle()
    {
        currentPhrase = originalPhrase;
        puzzleText.text = currentPhrase;
        feedbackText.text = "Match the hidden 5-step sequence!";
        currentStep = 0;
    }

    void HandlePress(string pressed)
    {
        if (pressed != correctSequence[currentStep])
        {
            feedbackText.text = "Wrong button! Sequence reset.";
            ResetPuzzle();
            return;
        }

        ApplyTransform(pressed);
        currentStep++;

        // Update messages
        if (currentStep >= correctSequence.Length)
        {
            feedbackText.text = "You solved it! The script returns!";
        }
        else
        {
            feedbackText.text = $"Good! Step {currentStep} / {correctSequence.Length} complete.";
        }
    }

    void ApplyTransform(string action)
    {
        if (action == "upper")
        {
            currentPhrase = currentPhrase.ToUpper();
        }
        else if (action == "lower")
        {
            currentPhrase = currentPhrase.ToLower();
        }
        else if (action == "strip")
        {
            currentPhrase = currentPhrase.Replace(" ", "");
        }

        // Update puzzle text display
        puzzleText.text = currentPhrase;
    }
}
