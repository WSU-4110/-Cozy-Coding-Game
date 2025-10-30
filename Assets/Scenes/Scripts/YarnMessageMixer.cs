using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class YarnMessageMixer : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI wolfText;
    public TextMeshProUGUI outputText;     // PlayerOutputText on the string line
    public Button[] wordButtons;           // assign WordButton1/2/3 here (in order)
    public Button stitchButton;            // KnitButton
    public TextMeshProUGUI feedbackText;   // FeedbackText

    [Header("Answer")]
    [Tooltip("What the player should build, spaces ignored when checking.")]
    public string correctAnswer = "'Hello' + ' ' + 'World'";

    private List<string> chosenWords = new List<string>();

    void Start()
    {
        if (wolfText != null)
            wolfText.text = "Let's stitch these words together to make a message! Click the yarn tokens in order.";

        // Hook up button clicks
        foreach (Button btn in wordButtons)
            btn.onClick.AddListener(() => OnWordClick(btn));

        if (stitchButton != null)
            stitchButton.onClick.AddListener(CheckAnswer);

        if (outputText != null)
            outputText.text = "";
        if (feedbackText != null)
            feedbackText.text = "";
    }

    void OnWordClick(Button btn)
    {
        // Read the label from the button's child TMP text
        var label = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (label == null) return;

        chosenWords.Add(label.text);
        outputText.text = string.Join(" + ", chosenWords);
    }

    void CheckAnswer()
    {
        string playerAnswer = string.Join(" + ", chosenWords);
        string normA = RemoveSpaces(playerAnswer);
        string normB = RemoveSpaces(correctAnswer);

        if (normA == normB)
        {
            feedbackText.text = "Output: Hello World";
            feedbackText.color = new Color(0.2f, 0.8f, 0.3f);
        }
        else
        {
            feedbackText.text = "Not quite right. Try again or re-order!";
            feedbackText.color = new Color(0.95f, 0.3f, 0.3f);
        }
    }

    string RemoveSpaces(string s)
    {
        return s.Replace(" ", "");
    }

    // Optional helper: call this from a Reset button if you add one
    public void ClearSelection()
    {
        chosenWords.Clear();
        if (outputText != null) outputText.text = "";
        if (feedbackText != null) feedbackText.text = "";
    }
}
