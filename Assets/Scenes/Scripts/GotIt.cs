using UnityEngine;
using TMPro;        // For TextMeshPro
using UnityEngine.UI; // For Button

public class GotIt : MonoBehaviour

{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI speechText;
    public Button ButtonGotIt;

    private string[] lines =
    {
        "In Python, to print words, you simply type \"print\" followed by (\"Insert Words Here\")",
        "Try it for yourself! Don't Forget to hit RUN to see your output!"
    };

    private int index = 0;

    void Start()
    {
        // Start with the first line
        speechText.text = lines[index];

        // Hook up button click
        ButtonGotIt.onClick.AddListener(NextLine);
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            speechText.text = lines[index];

            // Hide button on second line
            if (index == 1)
            {
                ButtonGotIt.gameObject.SetActive(false);
            }
        }
    }
}
