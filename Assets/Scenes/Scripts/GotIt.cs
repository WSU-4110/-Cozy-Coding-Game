using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GotIt : MonoBehaviour
{
    public RewardPopup rewardPopup; // drag your RewardPopup panel here in Inspector
    public string rewardName = "House Plant"; // example reward
    public Sprite rewardSprite; // drag the reward image here

    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI speechText;
    public Button ButtonGotIt;
    public TMP_InputField codeInput;  //drag your input field here in Inspector
    public Button RunButton;          //drag your RUN button here in Inspector

    private string[] lines =
    {
        "In Python, to print words, you simply type \"print\" followed by (\"Insert Words Here\")",
        "Try it for yourself! Don't Forget to hit RUN to see your output!"
    };

    private int index = 0;

    void Start()
    {
        //start with the first line
        speechText.text = lines[index];

        //hook up button click
        ButtonGotIt.onClick.AddListener(NextLine);

        //hook up RUN button
        RunButton.onClick.AddListener(CheckCode);
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            speechText.text = lines[index];

            //hide button on second line
            if (index == 1)
            {
                ButtonGotIt.gameObject.SetActive(false);
            }
        }
    }

    void CheckCode()
    {
        string playerCode = codeInput.text.Trim();
        string correctAnswer = "print(\"Hello World\")";

        if (playerCode == correctAnswer)
        {
            speechText.text = "Great job!! You got it!";

            // Show the reward popup
            if (rewardPopup != null)
            {
                rewardPopup.Show(rewardName, rewardSprite);
            }
        }
        else
        {
            speechText.text = "Hmm, try again!";
        }
    }
}
