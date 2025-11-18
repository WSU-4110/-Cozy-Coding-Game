using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LLP3Level : MonoBehaviour
{

    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI FeedbackText;

    public Button Button1;
    public Button Button2;
    public Button Button3;

    public TextMeshProUGUI Button1Text;
    public TextMeshProUGUI Button2Text;
    public TextMeshProUGUI Button3Text;

    private int levelPart = 0; // lvl1


    private string[] partInstruct = new string[6]
    {
        //Q's for sorting list
       "Select the correct version of this code that is meant to sort a list: \nmyList.sort(-1)",
       "Select the correct version of this code that is meant to reverse the order of a list: \nmyList.sort(reverse = True)",

       //Q's for copying list
       "Select the correct version of this code that is meant to make a copy of a list: \nlist2 = list1[]",
       "Select the correct version of this code that is meant to make a copy of a list: \nlist2 = list1[-1]",
       
       //Q's for  joining list
       "Select the correct version of this code that is meant to two lists into another list: \nlist1.append(list2)",
       "Select the correct version of this code that is meant to Add elements from one list into another: \nlist1.append(list2)"
    };

    private string[,] validAnswers = new string[,] 
    {
        //sorting list answers
       {"mylist.orderby(0)", "mylist.order()", "mylist.sort()"},
       {"mylist.sort(-1)", "mylist.sort(reverse = True)", "mylist.reverse()"},

       //cipying list answers
       {"list2 = list1", "list2 = list1.copy()", "list2.copy(list1)"},
       {"list2 = list(list1)", "list2 = list1", "list2 = list1.list()"},

       //joining list answers
       {"list3 = join(list1, list2)", "list3 = list1 + list2", "list3 = [list1, list2]"},
       {"list1.extend(list2)", "list1.join(list2)", "list1.push(list2)"}
    
    };

    private int[] validAnswersNum = new int[6] { 2, 2, 1, 0, 1, 0 };

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        FeedbackText.text = "Welcome to ListLevel Part 3!";
        QuestionText.text = "Question instructions will appear here...";

        Button1.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);

        Button1.onClick.AddListener(() => checkAnswer(0));
        Button2.onClick.AddListener(() => checkAnswer(1));
        Button3.onClick.AddListener(() => checkAnswer(2));

    }

    public void displayQuestion()
    {
        QuestionText.text = partInstruct[levelPart];
        Button1Text.text = validAnswers[levelPart, 0];
        Button2Text.text = validAnswers[levelPart, 1];
        Button3Text.text = validAnswers[levelPart, 2];

        Button1.gameObject.SetActive(true);
        Button2.gameObject.SetActive(true);
        Button3.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        levelPart = 0;
        FeedbackText.text = "Please select the correct code!";
        displayQuestion();


    }

    private void checkAnswer(int choice)
    {
        if(choice == validAnswersNum[levelPart])
        {
            FeedbackText.text = "Correct! Great Job!";
            levelPart++;

            if (levelPart < partInstruct.Length)
            {
                displayQuestion();
            }
            else
            {
                FeedbackText.text = "Congratulations! You've completed all List level parts!";
                Button1.gameObject.SetActive(false);
                Button2.gameObject.SetActive(false);
                Button3.gameObject.SetActive(false);
            }
        }
        else
        {
            FeedbackText.text = "Incorrect! Try again.";
        }
    }

    public void RestartLvl()
    {
        levelPart = 0;
        FeedbackText.text = "Welcome to ListLevel Part 3!";
        QuestionText.text = "Question instructions will appear here...";
        Button1.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        Button3.gameObject.SetActive(false);
    }


}
