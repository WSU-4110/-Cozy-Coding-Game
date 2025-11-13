using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    public Transform slotParent;    // Panel to hold slots
    public GameObject slotPrefab;   // UI prefab for a reward

    void Start()
    {
        string[] items = InventoryManager.GetItems();

        foreach (string itemID in items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            Image img = slot.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("Sprites/prizes/plant");
           
        }
    }
}
