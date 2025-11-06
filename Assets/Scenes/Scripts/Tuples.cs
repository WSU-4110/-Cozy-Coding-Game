using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Tuples : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text instructions;
    public TMP_Text feedback;
    public Button nextRoundButton;
    public Button homeButton; // Button to go back to home

    [Header("Apple Buttons Parent")]
    public Transform appleParent; // Parent containing the 4 apple buttons

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

    private List<Button> appleButtons = new List<Button>();

    void Start()
    {
        // Cache the apple buttons
        appleButtons.Clear();
        foreach (Transform child in appleParent)
        {
            appleButtons.Add(child.GetComponent<Button>());
        }

        nextRoundButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        feedback.text = "";
        SetupRound();
    }

    void SetupRound()
    {
        int[] tuple = rounds[currentRound];

        smallestValue = Mathf.Min(tuple);
        largestValue = Mathf.Max(tuple);

        instructions.text = "Pick the smallest and largest number!";

        // Assign numbers to the existing buttons and reset colors
        for (int i = 0; i < appleButtons.Count; i++)
        {
            Button btn = appleButtons[i];
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            btnText.text = tuple[i].ToString();

            btn.interactable = true;
            btn.GetComponent<Image>().color = Color.white; // reset color

            int capturedNum = tuple[i];
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnAppleClicked(capturedNum, btn));
        }

        selectedSmallest = null;
        selectedLargest = null;
        feedback.text = "";
        nextRoundButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
    }

    void OnAppleClicked(int number, Button btn)
    {
        if (selectedSmallest == null)
        {
            selectedSmallest = number;
            feedback.text = $"Smallest selected: {number}";
            btn.interactable = false;
            btn.GetComponent<Image>().color = Color.green; // highlight smallest
        }
        else if (selectedLargest == null)
        {
            selectedLargest = number;
            feedback.text = $"Largest selected: {number}";
            btn.interactable = false;
            btn.GetComponent<Image>().color = Color.red; // highlight largest

            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        if (selectedSmallest == smallestValue && selectedLargest == largestValue)
        {
            feedback.text = "Correct! Well done!";
        }
        else
        {
            feedback.text = $"Incorrect. Smallest: {smallestValue}, Largest: {largestValue}";
        }

        // If last round, show home button
        if (currentRound == rounds.Count - 1)
        {
            instructions.text = "Great Job on mastering tuples!";
            homeButton.gameObject.SetActive(true);
            nextRoundButton.gameObject.SetActive(false);
        }
        else
        {
            nextRoundButton.gameObject.SetActive(true);
        }
    }

    public void NextRound()
    {
        currentRound++;
        if (currentRound < rounds.Count)
        {
            SetupRound();
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene"); // Replace with your actual home scene name
    }
}


