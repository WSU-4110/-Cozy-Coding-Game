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

    private (string element, string creature)[] validRunes =
    {
        ("fire", "phoenix"),
        ("ice", "wolf"),
        ("wind", "serpent")
    };

    public void ChooseElement(string e)
    {
        element = e.ToLower();
        UpdateRunePreview();
    }

    public void ChooseCreature(string c)
    {
        creature = c.ToLower();
        UpdateRunePreview();
    }

    private void UpdateRunePreview()
    {
        string eText = string.IsNullOrEmpty(element) ? "___" : element;
        string cText = string.IsNullOrEmpty(creature) ? "___" : creature;

        runeText.text = $"The power of {eText} lies within {cText}.";
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

        bool isCorrect = false;
        foreach (var combo in validRunes)
        {
            if (element == combo.element && creature == combo.creature)
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            feedback.text = "The rune glows brightly — you restored it!";
        }
        else
        {
            feedback.text = "The symbols flicker... not quite right.";
        }
    }
}
