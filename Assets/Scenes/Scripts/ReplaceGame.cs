using UnityEngine;
using TMPro;

public class ReplaceGame : MonoBehaviour
{
    public TextMeshProUGUI signText;
    public TextMeshProUGUI feedback;

    private string[] brokenSigns = {
        "WELCXME  T  YARN TOWN!",
        "PYTHXN  IS  CXXL!",
        "CXDING  IS  FUN!"
    };

    private string[] fixedSigns = {
        "WELCOME T YARN TOWN!",
        "PYTHON IS COOL!",
        "CODING IS FUN!"
    };

    private int currentIndex = 0;

    void Start()
    {
        MakePuzzle();
    }

    public void Btn_ReplaceX()
    {
        signText.text = signText.text.Replace("X", "O");
        Check();
    }

    public void Btn_Strip()
    {
        signText.text = signText.text.Trim();
        Check();
    }

    public void Btn_FixSpaces()
    {
        signText.text = signText.text.Replace("  ", " ");
        Check();
    }

    public void Btn_TryAnother()
    {
        MakePuzzle();
    }

    void MakePuzzle()
    {
        currentIndex = Random.Range(0, brokenSigns.Length);
        signText.text = brokenSigns[currentIndex];
        feedback.text = "";
    }

    void Check()
    {
        if (signText.text == fixedSigns[currentIndex])
        {
            feedback.text = "<color=green>Fixed!</color>";
        }
        else
        {
            feedback.text = "";
        }
    }
}
