using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public static class InventoryManager
{
    private const string KEY = "Inventory";

    // Add a new item
    public static void AddItem(string itemID)
    {
        string current = PlayerPrefs.GetString(KEY, "");
        if (!string.IsNullOrEmpty(current))
            current += ","; // comma-separated
        current += itemID;
        PlayerPrefs.SetString(KEY, current);
        PlayerPrefs.Save();
    }

    // Get all items
    public static string[] GetItems()
    {
        string current = PlayerPrefs.GetString(KEY, "");
        if (string.IsNullOrEmpty(current)) return new string[0];
        return current.Split(',');
    }
}
