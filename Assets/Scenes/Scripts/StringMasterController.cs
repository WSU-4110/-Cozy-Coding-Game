using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StringMasterController : MonoBehaviour
{
    [Header("Wolf Dialogue")]
    public TextMeshProUGUI wolfText;

    [Header("Panels")]
    public GameObject replacePanel;
    public GameObject splitPanel;
    public GameObject casePanel;
    public GameObject slicePanel;
    public GameObject fstringPanel;

    [Header("Navigation")]
    public Button nextButton;

    private int currentStage = 0;

    void Start()
    {
        ShowStage(0);
        if (nextButton != null)
            nextButton.onClick.AddListener(NextStage);
    }

    void ShowStage(int stage)
    {
        // Disable all first
        replacePanel.SetActive(false);
        splitPanel.SetActive(false);
        casePanel.SetActive(false);
        slicePanel.SetActive(false);
        fstringPanel.SetActive(false);

        switch (stage)
        {
            case 0:
                replacePanel.SetActive(true);
                wolfText.text = "Let’s repair the village sign using replace() and strip()!";
                break;
            case 1:
                splitPanel.SetActive(true);
                wolfText.text = "Let’s separate and join words using split() and join()!";
                break;
            case 2:
                casePanel.SetActive(true);
                wolfText.text = "Let’s try changing text to UPPER or lower case!";
                break;
            case 3:
                slicePanel.SetActive(true);
                wolfText.text = "Hmm... looks like the message is reversed! Use slicing [::-1] to fix it.";
                break;
            case 4:
                fstringPanel.SetActive(true);
                wolfText.text = "Now let’s format text using f-strings and placeholders!";
                break;
            default:
                wolfText.text = "All puzzles complete! Great work!";
                nextButton.gameObject.SetActive(false);
                break;
        }
    }

    void NextStage()
    {
        currentStage++;
        ShowStage(currentStage);
    }
}
