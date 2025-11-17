using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CaseGame : MonoBehaviour
{
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedback;

    private string originalText;
    private int currentPhrase = 0;

    private string[] phrases = {
        "  hello, world!  ",
        "tHis Is A TeST",
        "  PYTHON Is Fun!  "
    };

    void Start()
    {
        LoadPhrase();
    }

    void LoadPhrase()
    {
        originalText = phrases[currentPhrase];
        inputText.text = originalText;
        outputText.text = "";
        feedback.text = "Try fixing this string!";
    }

    public void Btn_Upper()
    {
        outputText.text = originalText.ToUpper();
        feedback.text = "Nice! You used upper() to capitalize everything.";
    }

    public void Btn_Lower()
    {
        outputText.text = originalText.ToLower();
        feedback.text = "Good job! You used lower() to make it all lowercase.";
    }

    public void Btn_Strip()
    {
        outputText.text = originalText.Trim();
        feedback.text = "Perfect! You used strip() to remove spaces.";
    }

    public void Btn_TryAnother()
    {
        currentPhrase = (currentPhrase + 1) % phrases.Length;
        LoadPhrase();
    }
}
