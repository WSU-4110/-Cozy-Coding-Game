using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Reward
{
    public string id;    // e.g. "plant"
    public Sprite sprite;
}

public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;
    public List<Reward> rewards;

    private Dictionary<string, Sprite> rewardDict;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rewardDict = new Dictionary<string, Sprite>();
        foreach (var r in rewards)
            rewardDict[r.id] = r.sprite;
    }

    public Sprite GetSprite(string id)
    {
        if (rewardDict.ContainsKey(id)) return rewardDict[id];
        return null;
    }
}
