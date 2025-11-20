using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WolfDialogue : MonoBehaviour
{
    public TextMeshProUGUI wolfText;
    public TextMeshProUGUI codeText;
    public Button nextButton;
    public Button runButton;
    public TMP_InputField inputField;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI outputText; 
    private int dialogueIndex = 0;
    private bool tutorialOver = false;
    private bool inExercise = false;
    private bool waitingForInput = false;
    private int currentExercise = 0;

    private string[] dialogues =
    {
        "Welcome back! Today we’re going to learn all about STRINGS in Python.",
        "Strings are text surrounded by quotes, like this: \"Hello World\" or 'Hello World'.",
        "You can use quotes inside strings too. For example:\nprint(\"It's alright\") or print('He said \"Hi!\"')",
        "Now let’s practice. Try typing: print(\"Hello World\") and press RUN.",
        "Nice! You can also assign strings to variables. Try this:\na = \"Hello\"\nprint(a)",
        "Good work! Strings are like arrays. That means we can access individual letters.\nTry this:\na = \"Hello\"\nprint(a[1])",
        "You can even check if a word exists in a string using 'in'. Try this:\ntxt = \"The best things in life are free!\"\nprint(\"free\" in txt)",
        "We can also check if something is NOT in a string using 'not in'. Try this:\nprint(\"expensive\" not in txt)",
        "That’s all the basics! You’re now ready for some short practice exercises."
    };

    private string[][] expectedAnswers =
    {
        new string[] { "print(\"Hello World\")", "print('Hello World')" },
        new string[] { "a=\"Hello\"\nprint(a)", "a = \"Hello\"\nprint(a)", "a = \"Hello\" print(a)", "a=\"Hello\"print(a)" },
        new string[] { "a=\"Hello\"\nprint(a[1])", "a = \"Hello\"\nprint(a[1])", "a=\"Hello\"print(a[1])" },
        new string[] { "txt=\"The best things in life are free!\"\nprint(\"free\" in txt)", "txt = \"The best things in life are free!\"\nprint(\"free\" in txt)" },
        new string[] { "print(\"expensive\" not in txt)", "print('expensive' not in txt)" }
    };

    private string[] exampleOutputs =
    {
        "Hello World",
        "Hello",
        "e",
        "True",
        "True"
    };

    private string[] exercisePrompts =
    {
        "Exercise 1:\nWhat will this code print?\n\nx = 'Welcome'\nprint(x[3])\n\nType your answer below (just the output):",
        "Exercise 2:\nFill in the blank:\n\ntxt = 'The best things in life are free!'\nif 'free' ____ txt:\n    print('Yes, free is present')\n\n(Type your missing keyword)",
        "Exercise 3:\nWhat does this print?\n\nx = 'Hello World'\nprint(len(x))"
    };

    private string[] correctExerciseAnswers = { "c", "in", "11" };

    void Start()
    {
        nextButton.onClick.AddListener(OnNextClicked);
        runButton.onClick.AddListener(OnRunClicked);
        inputField.gameObject.SetActive(false);
        runButton.gameObject.SetActive(false);
        feedbackText.text = "";
        if (outputText != null) outputText.text = ""; // initialize
        ShowDialogue();
    }

    void ShowDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            wolfText.text = dialogues[dialogueIndex];
            codeText.text = "";
            feedbackText.text = "";
            inputField.text = "";
            inputField.gameObject.SetActive(false);
            runButton.gameObject.SetActive(false);
            if (outputText != null) outputText.text = "";

            // Activate input when player needs to type
            if (dialogues[dialogueIndex].Contains("Try typing:") || dialogues[dialogueIndex].Contains("Try this:"))
            {
                waitingForInput = true;
                inputField.gameObject.SetActive(true);
                runButton.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);
            }
            else
            {
                waitingForInput = false;
                nextButton.gameObject.SetActive(true);
            }
        }
        else
        {
            tutorialOver = true;
            StartExercises();
        }
    }

    void OnNextClicked()
    {
        dialogueIndex++;
        ShowDialogue();
    }

    void OnRunClicked()
    {
        string raw = inputField.text;
        string user = Normalize(raw);

        if (tutorialOver)
        {
            CheckExerciseAnswer(raw);
            return;
        }

        int expectedIndex = -1;
        if (dialogueIndex == 3) expectedIndex = 0;
        else if (dialogueIndex == 4) expectedIndex = 1;
        else if (dialogueIndex == 5) expectedIndex = 2;
        else if (dialogueIndex == 6) expectedIndex = 3;
        else if (dialogueIndex == 7) expectedIndex = 4;

        if (expectedIndex == -1)
        {
            feedbackText.text = "You don't need to type anything here yet.";
            return;
        }

        // 1. Accept exact output ("helloworld", "true", etc.)
        string expectedOut = Normalize(exampleOutputs[expectedIndex]);
        if (user.Contains(expectedOut))
        {
            AcceptAnswer(expectedIndex);
            return;
        }

        // 2. Accept any expected syntax variation
        foreach (string syntax in expectedAnswers[expectedIndex])
        {
            if (user.Contains(Normalize(syntax)))
            {
                AcceptAnswer(expectedIndex);
                return;
            }
        }

        // 3. Additional forgiveness: allow missing semicolons, double spaces, order changes
        if (user.Contains("print") && user.Contains(expectedOut))
        {
            AcceptAnswer(expectedIndex);
            return;
        }

        // Otherwise incorrect
        feedbackText.text = "Not quite right. Check your syntax and spacing.";
        if (outputText != null) outputText.text = "";
    }


    // Called when correct — reused for cleaner code
    void AcceptAnswer(int index)
    {
        feedbackText.text = "Correct! Great job!";
        waitingForInput = false;
        runButton.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);

        if (outputText != null)
            outputText.text = "Output:\n> " + exampleOutputs[index];
    }


    private string Normalize(string s)
    {
        return s.ToLower()
                .Replace(" ", "")
                .Replace("\"", "")
                .Replace("'", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("\t", "")
                .Replace(";", "")
                .Trim();
    }



    void StartExercises()
    {
        if (currentExercise < exercisePrompts.Length)
        {
            inExercise = true;
            wolfText.text = "Let's test what you’ve learned!";
            codeText.text = exercisePrompts[currentExercise];
            inputField.text = "";
            feedbackText.text = "";
            inputField.gameObject.SetActive(true);
            runButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
            if (outputText != null) outputText.text = "";
        }
        else
        {
            inExercise = false;
            wolfText.text = "Excellent work! You’ve finished the Python Strings tutorial!";
            codeText.text = "You can now move on to the mini-games.";
            inputField.gameObject.SetActive(false);
            runButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            if (outputText != null) outputText.text = "";
        }
    }

    void CheckExerciseAnswer(string userInput)
    {
        string correctAnswer = correctExerciseAnswers[currentExercise].Trim().ToLower();
        string userAnswer = userInput.Trim().ToLower();

        // Normalize more forgiving
        correctAnswer = Normalize(correctAnswer);
        userAnswer = Normalize(userAnswer);

        bool isCorrect = userAnswer == correctAnswer;

        if (isCorrect)
        {
            if (outputText != null)
            {
                if (currentExercise == 0) outputText.text = "Output:\n> c";
                else if (currentExercise == 1) outputText.text = "Output:\n> Yes, free is present";
                else if (currentExercise == 2) outputText.text = "Output:\n> 11";
            }

            currentExercise++;
            Invoke(nameof(StartExercises), 1.2f);
        }
        else
        {
            if (outputText != null) outputText.text = "";
        }

    }
}

