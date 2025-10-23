using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// inside NextSpeech()
if (dialogueIndex >= dialogueLines.Length)
{
    SceneManager.LoadScene("Variable Names");
}

