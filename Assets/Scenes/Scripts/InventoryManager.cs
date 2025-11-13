using UnityEngine;

public static class InventoryManager
{
    private const string KEY = "Inventory";

    public static void AddItem(string itemID)
    {
        string current = PlayerPrefs.GetString(KEY, "");
        if (!string.IsNullOrEmpty(current)) current += ",";
        current += itemID;
        PlayerPrefs.SetString(KEY, current);
        PlayerPrefs.Save();
    }

    public static string[] GetItems()
    {
        string current = PlayerPrefs.GetString(KEY, "");
        if (string.IsNullOrEmpty(current)) return new string[0];
        return current.Split(',');
    }
}
