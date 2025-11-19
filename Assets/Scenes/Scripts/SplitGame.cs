using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SplitGame : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI puzzleText;      
    public TMP_InputField inputText;        // Player's typed input
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedback;

    private string currentPuzzle = "";

    private string[] puzzleStrings =
    {
        "echoes,of,the,deep",
        "ancient,stones,remember",
        "whispers,from,the,abyss",
        "lost,rune,of,shadows",
        "forgotten,path,beneath",
        "mystic,crystals,singing",
        "engraved,secrets,slumber",
        "hollow,earth,awakens",
        "arcane,patterns,hidden",
        "shattered,glyphs,echo"
    };

    void Start()
    {
        GenerateNewPuzzle();
    }

    public void GenerateNewPuzzle()
    {
        int pick = Random.Range(0, puzzleStrings.Length);
        currentPuzzle = puzzleStrings[pick];

        puzzleText.text = currentPuzzle;     // Shows the scrambled ancient string
        outputText.text = "";
        feedback.text = "";
        inputText.text = "";                 // clears player input
    }

    public void Btn_SplitIt()
    {
        string input = inputText.text;

        if (string.IsNullOrEmpty(input))
        {
            feedback.text = "Enter some text first!";
            return;
        }

        string[] parts = input.Split(',');

        string result = "[";

        for (int i = 0; i < parts.Length; i++)
        {
            result += $"'{parts[i].Trim()}'";
            if (i < parts.Length - 1)
                result += ", ";
        }

        result += "]";

        outputText.text = result;

        if (input == currentPuzzle)
        {
            feedback.text = "Great! You decoded the ancient inscription!";
        }
        else
        {
            feedback.text = "Hmm… not quite right. Try matching the symbols exactly.";
        }
    }
}
