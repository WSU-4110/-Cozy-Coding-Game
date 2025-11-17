using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SplitGame : MonoBehaviour
{
    public TMP_InputField inputText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedback;

    public void Btn_SplitIt()
    {
        string input = inputText.text;

        if (string.IsNullOrEmpty(input))
        {
            feedback.text = "Enter some text first!";
            return;
        }

        // Perform the split operation
        string[] parts = input.Split(',');

        string result = "[";
        for (int i = 0; i < parts.Length; i++)
        {
            result += $"'{parts[i].Trim()}'";
            if (i < parts.Length - 1) result += ", ";
        }
        result += "]";

        outputText.text = result;
        feedback.text = "Nice! You split the string successfully.";
    }
}
