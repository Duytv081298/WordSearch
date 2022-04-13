using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableManager : SingletonComponent<SaveableManager>
{
    public void LoadSaveData(List<CategoryInfo> categoryInfos)
    {
        if (CheckExistData())
        {
            // Người chơi đã từng tham gia trờ chơi
        }
        else
        {
            // Người chơi chưa từng tham gia trờ chơi
            Dictionary<string, int> lastCompletedLevel = new Dictionary<string, int>();
            foreach (var category in categoryInfos)
            {
                lastCompletedLevel.Add(category.saveId, 0);
            }
            GameManager.Instance.LastCompletedLevels = lastCompletedLevel;
        }

    }


    private bool CheckExistData()
    {
        return PlayerPrefs.HasKey("Used_to_play");

    }

    public void SetString(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }

    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }
}
