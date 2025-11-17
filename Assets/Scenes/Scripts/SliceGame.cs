using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliceGame : MonoBehaviour
{
    public TextMeshProUGUI encodedText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedback;

    private string currentMessage;
    private int currentIndex = 0;

    private string[] encodedMessages = {
        "!dlroW olleH",
        "nohtyP nraeL",
        "snoitalutargnoC"
    };

    private string[] decodedMessages = {
        "Hello World!",
        "Learn Python",
        "Congratulations"
    };

    void Start()
    {
        LoadMessage();
    }

    void LoadMessage()
    {
        currentMessage = encodedMessages[currentIndex];
        encodedText.text = currentMessage;
        outputText.text = "";
        feedback.text = "Try decoding this message!";
    }

    public void Btn_Reverse()
    {
        char[] arr = currentMessage.ToCharArray();
        System.Array.Reverse(arr);
        outputText.text = new string(arr);
        feedback.text = "Nice! You used slicing [::-1] to reverse it!";
    }

    public void Btn_SliceStart()
    {
        string result = currentMessage.Substring(0, 5);
        outputText.text = result;
        feedback.text = "You sliced the first 5 characters!";
    }

    public void Btn_SliceEnd()
    {
        int len = currentMessage.Length;
        string result = currentMessage.Substring(len - 5);
        outputText.text = result;
        feedback.text = "You sliced the last 5 characters!";
    }

    public void Btn_TryAnother()
    {
        currentIndex = (currentIndex + 1) % encodedMessages.Length;
        LoadMessage();
    }
}
