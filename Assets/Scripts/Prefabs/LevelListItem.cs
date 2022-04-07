using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListItem : MonoBehaviour
{

    [SerializeField] private Text levelText = null;
    [SerializeField] private Image categoryIcon = null;
    [SerializeField] private Image completedIcon = null;
    [SerializeField] private Image lockedIcon = null;
    [SerializeField] private Image playIcon = null;

    private TextAsset levelFile = null;

    private bool isEvent = true;


    public void Initialize(TextAsset levelFile, int level)
    {
        this.levelFile = levelFile;

        HideAllIcons();

        levelText.text = "LEVEL " + (level + 1).ToString();
        CategoryInfo activeCategory = GameManager.Instance.GetActiveCategory();
        int activeLevel = GameManager.Instance.GetLastCompletedLevels()[activeCategory.saveId];

        categoryIcon.sprite = activeCategory.icon;

        if (level < activeLevel) SetCompleted();
        else if (level == activeLevel) SetPlayable();
        else
        {
            isEvent = false;
            SetLocked();
        }
        // this.categoryU = category;
        // nameText.text = category.displayName;
        // backgroundImage.color = category.categoryColor;
        // SetProgress(category);
        // SetLocked(category);
    }

    private void SetCompleted()
    {
        completedIcon.enabled = true;
    }

    private void SetLocked()
    {
        lockedIcon.enabled = true;
    }

    private void SetPlayable()
    {
        playIcon.enabled = true;
    }

    private void HideAllIcons()
    {
        completedIcon.enabled = false;
        lockedIcon.enabled = false;
        playIcon.enabled = false;
    }
    public void onClickLevel(){
        if(isEvent) {
            ScreenManager.Instance.ShowScreen("game");
            GameManager.Instance.SetActiveLevel(this.levelFile);
        }
    }
}
