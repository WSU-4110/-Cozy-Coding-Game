using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class YarnMessageMixer : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI wolfText;
    public TextMeshProUGUI outputText;
    public Button[] wordButtons;
    public Button stitchButton;
    public Button resetButton;
    public TextMeshProUGUI feedbackText;

    [Header("Word Bank Settings")]
    public string[] phrases = {
        "Hello World",
        "I Love Coding",
        "Good Morning Wolf",
        "Learn Python Strings"
    };

    private List<string> currentWords = new List<string>();
    private List<string> chosenWords = new List<string>();
    private string targetPhrase;
    private int roundIndex = 0;

    void Start()
    {
        if (stitchButton != null)
            stitchButton.onClick.AddListener(CheckAnswer);
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetRound);

        LoadNewPhrase();
    }

    void LoadNewPhrase()
    {
        chosenWords.Clear();
        currentWords.Clear();
        outputText.text = "";
        feedbackText.text = "";

        targetPhrase = phrases[roundIndex];
        string[] splitWords = targetPhrase.Split(' ');

        currentWords.AddRange(splitWords);
        currentWords.AddRange(new string[] { "Game", "Wolf", "Learn", "Fun" });
        ShuffleList(currentWords);

        wolfText.text = $"Let's stitch together: {targetPhrase}";

        // Setup buttons
        for (int i = 0; i < wordButtons.Length; i++)
        {
            if (i < currentWords.Count)
            {
                string localWord = currentWords[i];
                wordButtons[i].gameObject.SetActive(true);

                var tmpText = wordButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (tmpText != null)
                    tmpText.text = localWord;

                wordButtons[i].onClick.RemoveAllListeners();
                wordButtons[i].onClick.AddListener(() => AddWord(localWord));
            }
            else
            {
                wordButtons[i].gameObject.SetActive(false);
            }
        }

        UpdateVariableDisplay();
    }

    void AddWord(string word)
    {
        if (chosenWords.Count >= 3) return; // cap at 3
        chosenWords.Add(word);
        UpdateVariableDisplay();
    }

    void UpdateVariableDisplay()
    {
        // Assign words directly in order clicked
        string a = chosenWords.Count > 0 ? $"\"{chosenWords[0]}\"" : "\"\"";
        string b = chosenWords.Count > 1 ? $"\"{chosenWords[1]}\"" : "\"\"";
        string c = chosenWords.Count > 2 ? $"\"{chosenWords[2]}\"" : "\"\"";

        outputText.text = $"a = {a}\n" +
                          $"b = {b}\n" +
                          $"c = {c}";
    }

    void CheckAnswer()
    {
        if (chosenWords.Count < 2)
        {
            feedbackText.text = "Please pick at least 2 words first!";
            feedbackText.color = new Color(0.9f, 0.3f, 0.3f);
            return;
        }

        // Build full phrase and check
        string built = string.Join(" ", chosenWords);
        string finalVar = chosenWords.Count == 2 ? "c" : "d";

        // Build the code block
        string concatLine = chosenWords.Count == 2
            ? $"{finalVar} = a + \" \" + b"
            : $"{finalVar} = a + \" \" + b + \" \" + c";

        string result = chosenWords.Count == 2
            ? $"{chosenWords[0]} {chosenWords[1]}"
            : $"{chosenWords[0]} {chosenWords[1]} {chosenWords[2]}";

        outputText.text =
            $"a = \"{(chosenWords.Count > 0 ? chosenWords[0] : "")}\"\n" +
            $"b = \"{(chosenWords.Count > 1 ? chosenWords[1] : "")}\"\n" +
            (chosenWords.Count > 2 ? $"c = \"{chosenWords[2]}\"\n" : "") +
            $"\n{concatLine}\nprint({finalVar})\n\n>>> {result}";

        if (built == targetPhrase)
        {
            feedbackText.text = $"Output: {result}";
            feedbackText.color = new Color(0.2f, 0.8f, 0.3f);
            wolfText.text = "Nice work! Let's try the next one.";

            roundIndex++;
            if (roundIndex >= phrases.Length)
            {
                wolfText.text = "All challenges complete!";
                return;
            }

            Invoke(nameof(LoadNewPhrase), 3f);
        }
        else
        {
            feedbackText.text = $"Not quite.\nOutput was:\n{result}";
            feedbackText.color = new Color(0.9f, 0.3f, 0.3f);
        }
    }

    void ResetRound()
    {
        chosenWords.Clear();
        feedbackText.text = "";
        outputText.text = "";
        wolfText.text = $"Let's try again: {targetPhrase}";
        UpdateVariableDisplay();
    }

    void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
}
