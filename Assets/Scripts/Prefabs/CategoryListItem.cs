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
    private CategoryInfo category = null;


    private bool isEvent = true;
    public void Initialize(CategoryInfo category, int id)
    {
        this.category = category;
        nameText.text = category.displayName;
        iconImage.sprite = category.icon;
        backgroundImage.color = category.categoryColor;
        SetProgress(category);
        SetLocked(category);
    }
    void SetProgress(CategoryInfo category)
    {
        int totalLevels = category.levelFiles.Count;
        int numLevelsCompleted = GameManager.Instance.LastCompletedLevels.ContainsKey(category.saveId) ? GameManager.Instance.LastCompletedLevels[category.saveId] + 1 : 0;
        levelProgressBar.SetProgress((float)numLevelsCompleted / (float)totalLevels);
        levelProgressText.text = string.Format("{0} / {1}", numLevelsCompleted, totalLevels);
    }
    void SetLocked(CategoryInfo category)
    {
        bool isCategoryLocked = category.lockType == 0 ? false : true;

        progressBarContainer.SetActive(!isCategoryLocked);
        lockedContainer.SetActive(isCategoryLocked);
        isEvent = !isActiveAndEnabled;

        coinsUnlockContainer.SetActive(isCategoryLocked && category.lockType == CategoryInfo.LockType.Coins);
        keysUnlockContainer.SetActive(isCategoryLocked && category.lockType == CategoryInfo.LockType.Keys);
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
        if (category.lockType == 0)
        {
            GameManager.Instance.ActiveCategoryInfo = this.category;
            PopupContainer.Instance.ShowCategorySelectedPopup();
        }
        else
        {

        }
    }
}
