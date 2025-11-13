using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ListLevelPart2 : MonoBehaviour
{

    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI feedbackllp2;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    public Button Button4;
    public TextMeshProUGUI B1text;
    public TextMeshProUGUI B2text;
    public TextMeshProUGUI B3text; 
    public TextMeshProUGUI B4text;


    public GameObject Racer;
    public float movementRange = 50f;


    private int currentQuestions = 0;
    private Vector3 initialPos;


    private string[] question = new string[6]
    {
        "What is a List method for removing list items?",
        "What is the correct usage of 'remove' method to remove 'banana' from fruits = ['apple', 'banana', 'cherry']",
        "What is a correct syntax for looping through the items of a list?",
        "What is a correct syntax for looping through the items of a list?",
        "Consider the code: fruits = ['apple', 'banana', 'cherry']\r\nnewlist = [x for x in fruits if x == 'banana'], What will be the value of newlist?",
        "Consider the code: fruits = ['apple', 'banana  ', 'cherry']\r\nnewlist = ['apple' for x in fruits], What will be the value of newlist?"
    };

    private string[,] answers = new string[6, 4]
    {
        {"pop()","push()","delete()","remove()" },
        {"fruits.delete('banana')","fruits.remove('banana')","remove.fruits('banana')","fruits.push('banana')" },
        {"for x in ['apple', 'banana', 'cherry']\r\n  print(x)","for x in ['apple', 'banana', 'cherry']:\r\n  print(x)","foreach x in ['apple', 'banana', 'cherry']\r\n  print(x)","" },
        {"print(x) for x in ['apple', 'banana', 'cherry']","[print(x) for x in ['apple', 'banana', 'cherry']]","for x in ['apple', 'banana', 'cherry'] print(x)","" },
        {"['apple', 'cherry']","True","\"['banana']\"", "" },
        {"['apple']","['apple', 'apple', 'apple']","True","" }

    };

    private int[] rightAnswer = new int[6] { 0, 1, 1, 1, 2, 1 };

    void Start()
    {
        initialPos = Racer.transform.position;
        ButtonToggle(false);

        Button1.onClick.AddListener(Button1Clicked);
        Button2.onClick.AddListener(Button2Clicked);
        Button3.onClick.AddListener(Button3Clicked);
        Button4.onClick.AddListener(Button4Clicked);

    }

    public void StartRace()
    {
        feedbackllp2.text = "Begin!";
        currentQuestions = 0;
        ShowQuestion(currentQuestions);
        ButtonToggle(true);
    }

    void ShowQuestion(int index)
    {
        QuestionText.text = question[index];
        B1text.text = answers[index, 0];
        B2text.text = answers[index, 1];
        B3text.text = answers[index, 2];
        B4text.text = answers[index, 3];
        feedbackllp2.text = "Select an answer!";
    }
    
    void OnAnswerSelected(int buttonIndex)
    {
        if (buttonIndex == rightAnswer[currentQuestions])
        {
            feedbackllp2.text = "Correct!";

            Racer.transform.position += new Vector3(movementRange, 0, 0);

            currentQuestions++;

            if(currentQuestions < question.Length)
            {  
                ShowQuestion(currentQuestions);
            }
            else
            {
                feedbackllp2.text = "Congratulations!";
                ButtonToggle(false);
            }
        }
        else
        {
            feedbackllp2.text = "Incorrect! Try Again!";
        }
    }

    void ButtonToggle(bool state)
    {
        Button1.gameObject.SetActive(state);
        Button2.gameObject.SetActive(state);
        Button3.gameObject.SetActive(state);
        Button4.gameObject.SetActive(state);
    }

    public void RestartLvl()
    {
        feedbackllp2.text = "Begin!";
        currentQuestions = 0;
        ShowQuestion(currentQuestions);
        ButtonToggle(true);
        Racer.transform.position = initialPos;

    }

    void Button1Clicked()
    {
        OnAnswerSelected(0);
    }

    void Button2Clicked()
    {
        OnAnswerSelected(1);
    }

    void Button3Clicked()
    {
        OnAnswerSelected(2);
    }

    void Button4Clicked()
    {
        OnAnswerSelected(3);
    }
}
