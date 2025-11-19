using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // needed for IEnumerator

public class RewardPopup : MonoBehaviour
{
    public Image rewardImage;
    public TMP_Text rewardText;
    public Button collectButton;

    private string rewardID;

    void Start()
    {
        gameObject.SetActive(false); // Hide popup initially
        collectButton.onClick.AddListener(OnCollectClicked);
    }

    // Show the popup with the reward info
    public void Show(string rewardName, Sprite rewardSprite)
    {
        rewardID = rewardName;
        rewardText.text = rewardName;
        rewardImage.sprite = rewardSprite;
        gameObject.SetActive(true);

        // Animate scale without LeanTween
        StartCoroutine(ScaleIn());
    }

    private IEnumerator ScaleIn()
    {
        float duration = 0.4f;
        float time = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        transform.localScale = startScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = t * t * (3f - 2f * t); // smoothstep easing
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        transform.localScale = endScale;
    }

    void OnCollectClicked()
    {
        PlayerPrefs.SetString("LastReward", rewardID);
        SceneManager.LoadScene("chest"); // make sure this is your inventory scene
    }
}

