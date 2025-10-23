using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
{
    public TMP_InputField codeInputField;
    public GameObject GotItButton;
    public GameObject WolfSpeechbubble;

    public void StartCodingPhase()
    {
        if (GotItButton != null)
            GotItButton.SetActive(false);
        if (WolfSpeechbubble != null)
            WolfSpeechbubble.SetActive(false);

        
        if (codeInputField != null)
        {
            codeInputField.ActivateInputField(); 
            codeInputField.text = "\n"; 
            codeInputField.caretPosition = codeInputField.text.Length;
        }
    }
}
