using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryListItem : MonoBehaviour
{

    [SerializeField] private Text nameText = null;
    [SerializeField] private Image iconImage = null;
    [SerializeField] private Image backgroundImage = null;
    [SerializeField] private ProgressBar levelProgressBar = null;
    [SerializeField] private Text levelProgressText = null;

    [Space]

    [SerializeField] private GameObject progressBarContainer = null;
    [SerializeField] private GameObject lockedContainer = null;
    [SerializeField] private GameObject coinsUnlockContainer = null;
    [SerializeField] private GameObject keysUnlockContainer = null;
    [SerializeField] private GameObject iapUnlockContainer = null;

    [Space]

    [SerializeField] private Text coinsUnlockAmountText = null;
    [SerializeField] private Text keysUnlockAmountText = null;
    [SerializeField] private Text iapUnlockPriceText = null;
    public CategoryInfo categoryU = null;

    public string getText()
    {
        return this.nameText.text;
    }

    public void setText(string text)
    {
        this.nameText.text = text;
    }

    public void Initialize(CategoryInfo category)
    {
        this.categoryU = category;
        nameText.text = category.displayName;
        iconImage.sprite = category.icon;
        backgroundImage.color = category.categoryColor;
        SetProgress(category);
        SetLocked(category);
    }
    void SetProgress(CategoryInfo category)
    {
        int totalLevels = category.levelFiles.Count;
        int numLevelsCompleted = 1;

        // levelProgressBar.SetProgress((float)numLevelsCompleted / (float)totalLevels);
        levelProgressText.text = string.Format("{0} / {1}", numLevelsCompleted, totalLevels);
    }
    void SetLocked(CategoryInfo category)
    {

        bool isCategoryLocked = category.lockType == 0 ? false : true;


        progressBarContainer.SetActive(!isCategoryLocked);
        lockedContainer.SetActive(isCategoryLocked);

        // Debug.Log(category.unlockAmount);
        // Debug.Log(JsonUtility.ToJson(category.lockType));
        // Debug.Log(JsonUtility.ToJson(CategoryInfo.LockType.Coins));

        switch (category.lockType)
        {
            case CategoryInfo.LockType.Coins:
                coinsUnlockContainer.SetActive(true);
                coinsUnlockAmountText.text = "x " + category.unlockAmount;
                break;
            case CategoryInfo.LockType.Keys:
                keysUnlockContainer.SetActive(true);
                keysUnlockAmountText.text = "x " + category.unlockAmount;
                break;
            case CategoryInfo.LockType.IAP:
                // SetIAPPrice(category.iapProductId);
                break;
        }
    }

    public void Onclick()
    {
        Debug.Log(JsonUtility.ToJson(categoryU));
        GameManager.Instance.SetCategory(categoryU);
        PopupContainer.Instance.ShowCategorySelectedPopup();
        // ScreenManager.Instance.ChangeGameScreen();
        Debug.Log(GameManager.Instance.GetCategory());
    }
}