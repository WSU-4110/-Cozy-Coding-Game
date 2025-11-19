using UnityEngine;
using TMPro;

public class SliceGame : MonoBehaviour
{
    public TextMeshProUGUI encodedText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedback;

    private int currentIndex = 0;

    private string[] encodedMessages = {
        // Reverse puzzles
        "!noitcetorP fo enots ehT",
        "!tahw sdniw tneicnA eht yrraC",
        "!reveN nrut eht yek ot tsap",

        // First-half puzzles
        "GuardianSpirit##whispersBelow",
        "MoonBlessing__echoesDeep",
        "SacredFlameRises%%shadowFalls",

        // Last-half puzzles
        "runesXXmysticTruthAwakens",
        "sealKey##PathOpensNow",
        "deepCave++AncientMemoryReturns"
    };

    private string[] correctDecoded = {
        "The stone of Protection",
        "Carry the Ancient winds!",
        "Never turn the key to past",

        "GuardianSpirit",
        "MoonBlessing",
        "SacredFlameRises",

        "mysticTruthAwakens",
        "PathOpensNow",
        "AncientMemoryReturns"
    };

    private enum SliceType { Reverse, FirstHalf, LastHalf }
    private SliceType[] puzzleSolution = {
        SliceType.Reverse,
        SliceType.Reverse,
        SliceType.Reverse,

        SliceType.FirstHalf,
        SliceType.FirstHalf,
        SliceType.FirstHalf,

        SliceType.LastHalf,
        SliceType.LastHalf,
        SliceType.LastHalf
    };

    void Start()
    {
        LoadPuzzle();
    }

    void LoadPuzzle()
    {
        outputText.text = "";
        encodedText.text = encodedMessages[currentIndex];
        feedback.text = "These symbols feel ancient… try slicing the message!";
    }

    // ---------------- BUTTONS ----------------

    public void Btn_Reverse()
    {
        string result = ReverseString(encodedMessages[currentIndex]);
        outputText.text = result;
        CheckResult(result, SliceType.Reverse);
    }

    public void Btn_FirstHalf()
    {
        string msg = encodedMessages[currentIndex];
        string result = msg.Substring(0, msg.Length / 2);
        outputText.text = result;
        CheckResult(result, SliceType.FirstHalf);
    }

    public void Btn_LastHalf()
    {
        string msg = encodedMessages[currentIndex];
        string result = msg.Substring(msg.Length / 2);
        outputText.text = result;
        CheckResult(result, SliceType.LastHalf);
    }

    public void Btn_TryAnother()
    {
        currentIndex = (currentIndex + 1) % encodedMessages.Length;
        LoadPuzzle();
    }

    // ---------------- CHECKING ----------------

    private void CheckResult(string playerOutput, SliceType usedType)
    {
        if (usedType == puzzleSolution[currentIndex] &&
            playerOutput == correctDecoded[currentIndex])
        {
            feedback.text = "The rune glows… you decoded its secret!";
        }
        else if (usedType != puzzleSolution[currentIndex])
        {
            feedback.text = "Hmm… the rune resists. Try a different slice.";
        }
        else
        {
            feedback.text = "You're close… but the symbols aren’t aligned.";
        }
    }

    private string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        System.Array.Reverse(arr);
        return new string(arr);
    }
}
