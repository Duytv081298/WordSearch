using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableManager : SingletonComponent<SaveableManager>
{


    public void LoadSaveData(List<CategoryInfo> categoryInfos)
    {
        Dictionary<string, int> LastCompletedLevel = new Dictionary<string, int>();
        foreach (var category in categoryInfos)
        {
            LastCompletedLevel.Add(category.saveId, 0);
        }
        GameManager.Instance.SetLastCompletedLevels(LastCompletedLevel);

    }
}
