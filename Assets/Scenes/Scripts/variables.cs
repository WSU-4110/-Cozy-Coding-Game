using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    public TextMeshProUGUI speechvar;
    public Button continueButton;
    public GameObject arrow;

    private int dialogueIndex = 0;

    private string[] dialogueLines = {
        "Variables are like containers that hold data.",
        "They can store numbers, text, or other values.",
        "There are many versions of Variables to learn",
      
    };
    void Start()
    {
        // Show the first line
        speechvar.text = dialogueLines[0];
        arrow.SetActive(false);
    }

    public void NextSpeech()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueLines.Length)
        {
            speechvar.text = dialogueLines[dialogueIndex];
        }
        else
        {
            speechvar.text = "Let's start with variable names!";
            continueButton.gameObject.SetActive(false);
            arrow.SetActive(true);

        }
    }
}


