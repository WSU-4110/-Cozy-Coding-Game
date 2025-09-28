using UnityEngine;
using TMPro;        //for TextMeshPro
using UnityEngine.UI; //for Button
using UnityEngine.SceneManagement; //needed for scene changes


public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI buttonText; //drag Yes TXT here
    public TextMeshProUGUI speechText; //drag "Intro TXT" here
    public Button yesButton;           //drag "Yes Button" here

    private string[] lines =
    {
        "Hello Traveler... So you want to learn to code in Python?",
        "Okay! Let's get started!"
    };

    private int index = 0;

    void Start()
    {
        //start with the first line
        speechText.text = lines[index];

        //hook up button click
        yesButton.onClick.AddListener(NextLine);
    }


    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            speechText.text = lines[index];

            //change button text on second line
            if (index == 1)
            {
                buttonText.text = "Continue";
            }
        }
        else
        {
            //load next scene when button clicked after last line
            SceneManager.LoadScene("Home"); 
        }
    }
}
