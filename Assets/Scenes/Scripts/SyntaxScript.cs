using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CodeRunner : MonoBehaviour
{
    public TMP_InputField codeInput;
    public TextMeshProUGUI outputText;

    public void RunCode()
    {
        string userCode = codeInput.text.Trim();

        // Very basic check for Hello World
        if (userCode == "print(\"Hello World\")" || userCode == "print('Hello World')")
        {
            outputText.text = "Output: Hello World";
        }
        else
        {
            outputText.text = "Try again! Hint: print(\"Hello World\")";
        }
    }
}
