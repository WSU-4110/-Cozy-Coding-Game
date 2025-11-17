using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RuneGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI runeText;
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI feedback;

    private string element = "";
    private string creature = "";

    private string correctElement = "fire";
    private string correctCreature = "wolf";

    public void ChooseElement(string e)
    {
        element = e;
        UpdateRunePreview();
    }

    public void ChooseCreature(string c)
    {
        creature = c;
        UpdateRunePreview();
    }

    private void UpdateRunePreview()
    {
        runeText.text = $"The power of {(string.IsNullOrEmpty(element) ? "{element}" : element)} lies within {(string.IsNullOrEmpty(creature) ? "{creature}" : creature)}.";
    }

    public void Btn_Invoke()
    {
        if (string.IsNullOrEmpty(element) || string.IsNullOrEmpty(creature))
        {
            feedback.text = "The rune is incomplete...";
            outputText.text = "";
            return;
        }

        string result = $"The power of {element} lies within {creature}.";
        outputText.text = result;

        if (element == correctElement && creature == correctCreature)
        {
            feedback.text = "The rune glows brightly — you restored it!";
        }
        else
        {
            feedback.text = "The symbols flicker... not quite right.";
        }
    }
}
