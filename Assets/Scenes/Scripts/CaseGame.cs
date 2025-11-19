using UnityEngine;
using TMPro;

public class CaseGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI inputText;      // Shows the messed-up string
    public TextMeshProUGUI outputText;     // Shows corrected string
    public TextMeshProUGUI feedback;       // Wolf's comments
    public TextMeshProUGUI puzzleText;     

    private string originalText;
    private int currentPhrase = 0;

    private string[] phrases =
    {
        "ThE AnCieNt RuNeS gLoW FaiNtLy",
        "dEeP iN tHe cAvE, a vOiCe WhiSpErS",
        "tHe LoSt ScRiPt rEtUrNs",
        "mYsTiC sYmBoLs cOvEr tHe wAlLs",
        "aNcIeNt kNoWLeDgE sHaLl rIsE"
    };

    void Start()
    {
        LoadRandomPhrase();
    }

    public void LoadRandomPhrase()
    {
        int newIndex = Random.Range(0, phrases.Length);
        currentPhrase = newIndex;

        originalText = phrases[currentPhrase];

        inputText.text = originalText;
        puzzleText.text = originalText;

        outputText.text = "";
        feedback.text = "";
    }

    // Transformations
    public void ApplyUpper()
    {
        outputText.text = originalText.ToUpper();
        feedback.text = "The runes shine with renewed power!";
    }

    public void ApplyLower()
    {
        outputText.text = originalText.ToLower();
        feedback.text = "Soft echoes fill the cave… the text stabilizes.";
    }

    public void ApplyStrip()
    {
        outputText.text = originalText.Trim();
        feedback.text = "Unwanted glyphs fade into dust.";
    }

    // Reloads a new phrase
    public void TryAnother()
    {
        LoadRandomPhrase();
    }
}
