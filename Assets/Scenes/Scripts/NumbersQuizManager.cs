using UnityEngine;
using UnityEngine.UI;
using TMPro; // Needed for TextMeshPro

public class NumbersQuizManager : MonoBehaviour
{
    // Public references we need to set in the Unity Inspector
    public Button intButton;
    public Button longButton; // This is the correct answer
    public Button floatButton;
    public GameObject feedbackTextObject; // The object that holds the feedback text
    public TextMeshProUGUI feedbackText; // The actual text component

    // The correct answer for the current question
    private string correctAnswer = "long";

    void Start()
    {
        // 1. Ensure the feedback text is hidden at the start
        feedbackTextObject.SetActive(false);

        // 2. Assign the click function to each button
        intButton.onClick.AddListener(() => CheckAnswer("int"));
        longButton.onClick.AddListener(() => CheckAnswer("long"));
        floatButton.onClick.AddListener(() => CheckAnswer("float"));
    }

    public void CheckAnswer(string clickedAnswer)
    {
        // Stop processing multiple clicks until feedback is done
        DisableButtons(); 

        // Check if the clicked answer matches the correct answer
        if (clickedAnswer == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green; // Set color to green for correct
        }
        else
        {
            feedbackText.text = "Incorrect!";
            feedbackText.color = Color.red; // Set color to red for incorrect
        }

        // Show the feedback text
        feedbackTextObject.SetActive(true);
    }

    // Disables button interaction after an answer is chosen
    private void DisableButtons()
    {
        intButton.interactable = false;
        longButton.interactable = false;
        floatButton.interactable = false;
    }

    // You would later add a function here to load the next question or level
}