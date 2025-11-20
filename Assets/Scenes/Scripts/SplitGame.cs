using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SplitGame : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI puzzleText;
    public TMP_InputField inputText;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI outputText;

    private int round = 0;

    // Scrambled phrases (no commas, no spaces)
    private string[] scrambledPhrases =
    {
        "engravedsecretslumber",
        "crystallanternforest",
        "whisperancientmemory"
    };

    // Correct solutions (3 words each)
    private string[] correctSolutions =
    {
        "engraved,secret,slumber",
        "crystal,lantern,forest",
        "whisper,ancient,memory"
    };

    void Start()
    {
        LoadRound();
    }

    // Load the current round
    void LoadRound()
    {
        puzzleText.text = scrambledPhrases[round];
        feedbackText.text = "";
        outputText.text = "";
        inputText.text = "";
    }

    // Called when player presses SPIT IT button
    public void OnSplitClicked()
    {
        string user = Normalize(inputText.text);
        string answer = Normalize(correctSolutions[round]);

        if (user == answer)
        {
            // Good!
            string[] words = correctSolutions[round].Split(',');
            outputText.text = $"[ {words[0]}, {words[1]}, {words[2]} ]";
            feedbackText.text = "Great job!";

            round++;

            if (round < scrambledPhrases.Length)
                Invoke(nameof(LoadRound), 1f);
            else
                EndGame();
        }
        else
        {
            feedbackText.text = "Not quite. Try splitting into 3 words.";
            outputText.text = "";
        }
    }

    void EndGame()
    {
        puzzleText.text = "You decoded all the messages!";
        feedbackText.text = "Great work!";
        outputText.text = "";
        inputText.gameObject.SetActive(false);
    }

    // Makes text easy to compare
    string Normalize(string s)
    {
        return s.ToLower()
            .Replace(" ", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("\"", "")
            .Trim();
    }
}
