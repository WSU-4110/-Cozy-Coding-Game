using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Tuples : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI instructions;
    public TextMeshProUGUI feedback;
    public Button continueButton;
    public Button nextRoundButton;
    public Button homeButton;

    [Header("Apple Buttons")]
    public List<Button> appleButtons;

    private int introStep = 0;

    private string[] introMessages = new string[]
    {
        "A tuple in Python is an ordered collection of values that cannot be changed.",
        "Example: (4, 2, 7, 1)",
        "In this game, you'll practice reading tuples by choosing the smallest and largest values!"
    };

    private List<int[]> rounds = new List<int[]>()
    {
        new int[] {4, 2, 7, 1},
        new int[] {9, 3, 6, 5}
    };

    private int currentRound = 0;
    private int smallestValue;
    private int largestValue;

    private int? selectedSmallest = null;
    private int? selectedLargest = null;

    void Start()
    {
        // Hide apples until intro is finished
        foreach (var apple in appleButtons)
            apple.gameObject.SetActive(false);

        feedback.text = "";
        nextRoundButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);

        continueButton.onClick.AddListener(NextIntroStep);
        instructions.text = introMessages[introStep];
    }

    void NextIntroStep()
    {
        introStep++;

        if (introStep < introMessages.Length)
        {
            instructions.text = introMessages[introStep];
        }
        else
        {
            StartGame();
        }
    }

    void StartGame()
    {
        continueButton.gameObject.SetActive(false);

        foreach (var apple in appleButtons)
            apple.gameObject.SetActive(true);

        SetupRound();
    }

    void SetupRound()
    {
        int[] tuple = rounds[currentRound];

        smallestValue = Mathf.Min(tuple);
        largestValue = Mathf.Max(tuple);

        instructions.text = "Pick the smallest then the largest number.";
        feedback.text = "";

        for (int i = 0; i < appleButtons.Count; i++)
        {
            Button btn = appleButtons[i];
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            btnText.text = tuple[i].ToString();

            btn.interactable = true;
            btn.GetComponent<Image>().color = Color.white;

            int capturedNum = tuple[i];
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnAppleClicked(capturedNum, btn));
        }

        selectedSmallest = null;
        selectedLargest = null;

        nextRoundButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
    }

    void OnAppleClicked(int number, Button btn)
    {
        if (selectedSmallest == null)
        {
            selectedSmallest = number;
            feedback.text = $"Smallest selected: {number}";
            btn.GetComponent<Image>().color = Color.green;
            btn.interactable = false;
        }
        else if (selectedLargest == null)
        {
            selectedLargest = number;
            feedback.text = $"Largest selected: {number}";
            btn.GetComponent<Image>().color = Color.red;
            btn.interactable = false;

            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        if (selectedSmallest == smallestValue && selectedLargest == largestValue)
        {
            feedback.text = "Correct!";

            if (currentRound == rounds.Count - 1)
            {
                instructions.text = "Great job mastering tuples!";
                homeButton.gameObject.SetActive(true);
            }
            else
            {
                nextRoundButton.gameObject.SetActive(true);
            }
        }
        else
        {
            feedback.text = "Try again!";
            ResetForRetry();
        }
    }

    void ResetForRetry()
    {
        selectedSmallest = null;
        selectedLargest = null;

        foreach (var btn in appleButtons)
        {
            btn.interactable = true;
            btn.GetComponent<Image>().color = Color.white;
        }
    }

    public void NextRound()
    {
        currentRound++;
        SetupRound();
    }

    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}



