using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReplaceGame : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI signText;         // Text on the stone sign / panel
    public TextMeshProUGUI feedbackText;     // Wolf comments
    public TextMeshProUGUI outputText;       // Optional "current string" display

    public Button replaceButton;             // .replace()
    public Button stripButton;               // .strip()
    public Button fixSpacesButton;           // fix spaces / clean up
    public Button nextButton;                // Next puzzle (if you have one)

    private int currentPuzzle = 0;
    private string workingText;

    // 1) Text the player sees at the start
    private string[] brokenPuzzles = {
        "  heXXo,   worXd!  ",
        "  PYThon   iz   fXn  ",
        "  lXsts   Xre   pXwerful  "
    };

    // 2) How it should look AFTER using .replace()
    private string[] afterReplace = {
        "  hello,   world!  ",
        "  Python   is   fun  ",
        "  lists   are   powerful  "
    };

    // 3) Final target text after strip + fixing spaces
    private string[] finalTargets = {
        "hello, world!",
        "Python is fun",
        "lists are powerful"
    };

    void Start()
    {
        // Hook up buttons
        replaceButton.onClick.AddListener(DoReplace);
        stripButton.onClick.AddListener(DoStrip);
        fixSpacesButton.onClick.AddListener(DoFixSpaces);

        if (nextButton != null)
            nextButton.onClick.AddListener(NextPuzzle);

        LoadPuzzle();
    }

    void LoadPuzzle()
    {
        workingText = brokenPuzzles[currentPuzzle];
        signText.text = workingText;

        if (outputText != null)
            outputText.text = workingText;

        feedbackText.text = "Let's repair the village sign using string methods!";
    }

    void DoReplace()
    {
        workingText = afterReplace[currentPuzzle];
        UpdateTextAndCheck();
        feedbackText.text = "Nice! replace() fixed the wrong characters.";
    }

    // .strip() – trim leading & trailing spaces
    void DoStrip()
    {
        workingText = workingText.Trim();
        UpdateTextAndCheck();
        feedbackText.text = "strip() cleaned the edges of the string.";
    }

    // Fix spaces – compress multiple spaces to single spaces
    void DoFixSpaces()
    {
        while (workingText.Contains("  "))
        {
            workingText = workingText.Replace("  ", " ");
        }

        UpdateTextAndCheck();
        feedbackText.text = "Much better spacing now!";
    }

    // Update UI and see if we reached the final target
    void UpdateTextAndCheck()
    {
        signText.text = workingText;

        if (outputText != null)
            outputText.text = workingText;

        string currentClean = workingText;
        string target = finalTargets[currentPuzzle];

        if (currentClean == target)
        {
            feedbackText.text =
                "Perfect! The sign now reads:\n\"" + target + "\"";
        }
    }

    public void NextPuzzle()
    {
        currentPuzzle++;

        if (currentPuzzle >= brokenPuzzles.Length)
        {
            feedbackText.text =
                "You fixed all the signs! The village loves your string skills!";
            return;
        }

        LoadPuzzle();
    }
}
