using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ListLevel1Script : MonoBehaviour
{
    public TMP_InputField codeInput; 
    public TextMeshProUGUI ListLevel1Instruct; //Wolf Text Bubble
    public TextMeshProUGUI ListLevel1Output; //Output Text Box


    private int levelPart= 0; //lvl 1


    private string[] partInstruct = 
    {
        "Part 1(Accessing list): Access the second item in the list, myList. ",
        "Part 2(Changing a list): Change the First item in myList to 'pear'.",
        "Part 3(Adding to a list): Insert 'plum' at index 1 of myList."

    };

    private string[] validInputs = 
    {
        "myList[1]",
        "myList[0] = 'pear'",
        "myList.insert(1, 'plum')"
    };

    private string[] partHint = 
    {
        "Lists are indexxed, the first item being index 0, use myList[index #].",
        "To change an item in a list, use myList[index #] = 'new item'.",
        "To add an item in a list, use myList.insert(index #, 'new item')."
    };

    private string[] partOutputs = 
    {
        "banana",
        "['pear', 'banana', 'cherry']",
        "['pear', 'plum', 'cherry']"
    };
    void Start()
    {
        ListLevel1Instruct.text = partInstruct[levelPart]; //first 
    }


    public void RunCode()
    {
        string userCode = codeInput.text.Trim();

        if (userCode == validInputs[levelPart])
        {
            ListLevel1Output.text = "Correct!\nOutput: " + partOutputs[levelPart];
            levelPart++;

            if (levelPart < partHint.Length) //are we stil in the range of levels available?
            {
                ListLevel1Instruct.text = partInstruct[levelPart];
                codeInput.text = ""; //If we still have levels left, clear input box
                

            }
            else
            {
                ListLevel1Instruct.text = "You have completed Python Lists Level 1!";
                ListLevel1Output.text = "Great Job!\nOutput: " + partOutputs[2];
            }
        }
        else
        {
            ListLevel1Output.text = "Try Again! Hint: " + partHint[levelPart];
        }
    }

    public void RestartLvl()
    {
        levelPart = 0;
        ListLevel1Instruct.text = partInstruct[levelPart];
        ListLevel1Output.text = "";
        codeInput.text = "";
    }
    
}
