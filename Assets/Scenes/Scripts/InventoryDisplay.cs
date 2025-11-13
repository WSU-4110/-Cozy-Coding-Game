using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Transform slotParent;
    public GameObject slotPrefab;

    void Start()
    {
        string[] items = InventoryManager.GetItems();

        foreach (string itemID in items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            Image img = slot.GetComponent<Image>();

            // If using RewardManager:
            Sprite sprite = RewardManager.instance.GetSprite(itemID);

            // If using pre-assigned prefab, you can skip RewardManager:
            // Sprite sprite = img.sprite; // already assigned in prefab

            if (sprite != null) img.sprite = sprite;
            else Debug.LogError("Could not find sprite for " + itemID);
        }
    }
}

