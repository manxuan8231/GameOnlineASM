using System;
using UnityEngine;

public class GameData
{
    public static void SaveData(PlayerData playerData)
    {
    
        var json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("PlayerData", json);

    }
    public static PlayerData LoadData()
    {
        var json = PlayerPrefs.GetString("PlayerData");
        if (string.IsNullOrEmpty(json))
        {
            return null;
        }        
        return JsonUtility.FromJson<PlayerData>(json);
    }

}
[Serializable]
public class PlayerData
{
    public string playerName;
    public string playerClass;
    public int health;
    public float speed;
}
