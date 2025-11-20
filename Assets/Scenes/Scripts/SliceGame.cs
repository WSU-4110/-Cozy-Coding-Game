using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliceGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI encodedText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedbackText;

    [Header("Buttons")]
    public Button reverseButton;
    public Button sliceStartButton;
    public Button sliceEndButton;
    public Button tryAnotherButton;

    // ------------------------------
    //   PHRASES & THEIR SOLUTIONS
    // ------------------------------

    private class SlicePuzzle
    {
        public string scrambled;
        public string correct;
        public string correctSliceStart;  // e.g. "[0:5]"
        public string correctSliceEnd;    // e.g. "[-5:]"
        public bool needsReverse;         // whether reverse is needed before slicing

        public SlicePuzzle(string scrambled, string correct, string start, string end, bool rev)
        {
            this.scrambled = scrambled;
            this.correct = correct;
            this.correctSliceStart = start;
            this.correctSliceEnd = end;
            this.needsReverse = rev;
        }
    }

    private SlicePuzzle[] puzzles;
    private SlicePuzzle current;
    private string workingText;
    private bool usedStart, usedEnd;

    void Start()
    {
        puzzles = new SlicePuzzle[]
        {
            new SlicePuzzle("dlroW olleH", "Hello World", "[::-1]", "[::]", true),
            new SlicePuzzle("gnidoc evol I", "I love coding", "[::-1]", "[::]", true),
            new SlicePuzzle("!!!noitcetorp fo enots ehT", "The stone of protection!!!", "[::-1]", "[::]", true),

            new SlicePuzzle("xxxxHelloWorldxxxx", "HelloWorld", "[4:]", "[:-4]", false),
            new SlicePuzzle("___PythonRocks___", "PythonRocks", "[3:]", "[:-3]", false),
            new SlicePuzzle("###SliceGame###", "SliceGame", "[3:]", "[:-3]", false)
        };

        // Button listeners
        reverseButton.onClick.AddListener(OnReverse);
        sliceStartButton.onClick.AddListener(OnSliceStart);
        sliceEndButton.onClick.AddListener(OnSliceEnd);
        tryAnotherButton.onClick.AddListener(LoadRandomPuzzle);

        LoadRandomPuzzle();
    }

    // -----------------------------
    //      LOAD NEW PUZZLE
    // -----------------------------
    void LoadRandomPuzzle()
    {
        current = puzzles[Random.Range(0, puzzles.Length)];

        workingText = current.scrambled;
        encodedText.text = workingText;

        outputText.text = "";
        feedbackText.text = "";

        usedStart = false;
        usedEnd = false;
    }

    void OnReverse()
    {
        workingText = ReverseString(workingText);
        outputText.text = workingText;
        CheckCompletion();
    }

    void OnSliceStart()
    {
        usedStart = true;

        if (current.correctSliceStart.Contains("[4:")) workingText = workingText.Substring(4);
        else if (current.correctSliceStart.Contains("[3:")) workingText = workingText.Substring(3);
        else if (current.correctSliceStart.Contains("[::-1]")) workingText = ReverseString(workingText);

        outputText.text = workingText;
        CheckCompletion();
    }

    void OnSliceEnd()
    {
        usedEnd = true;

        if (current.correctSliceEnd.Contains("[:-4]")) workingText = workingText.Substring(0, workingText.Length - 4);
        else if (current.correctSliceEnd.Contains("[:-3]")) workingText = workingText.Substring(0, workingText.Length - 3);
        else if (current.correctSliceEnd.Contains("[::]")) { /* do nothing */ }

        outputText.text = workingText;
        CheckCompletion();
    }

    // -----------------------------
    //     CHECK IF SOLVED
    // -----------------------------
    void CheckCompletion()
    {
        // Reverse-only puzzles
        if (current.needsReverse)
        {
            if (workingText == current.correct)
            {
                feedbackText.text = "Correct! Message decoded.";
            }
            else
            {
                feedbackText.text = "";
            }
            return;
        }

        // Slice puzzles require BOTH start & end
        if (usedStart && usedEnd)
        {
            if (workingText == current.correct)
            {
                feedbackText.text = "Great! You decoded the phrase!";
            }
            else
            {
                feedbackText.text = "Incorrect slice combination. Try again.";
            }
        }
        else
        {
            feedbackText.text = "";
        }
    }

    // -----------------------------
    //   REVERSE HELPER FUNCTION
    // -----------------------------
    string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        System.Array.Reverse(arr);
        return new string(arr);
    }
}
