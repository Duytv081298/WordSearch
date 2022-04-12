using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelectedPopup : MonoBehaviour
{


    [SerializeField] private CanvasGroup selectModeContainer = null;
    [SerializeField] private CanvasGroup SelectDifficultyContainer = null;

    [SerializeField] private bool activePlayCasual = false;
    void Start()
    { }
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     // Debug.Log("click");
        //     // btn_Close_X.onClick.AddListener(ClosePopupCategorySelected);
        //     // btn_Close.onClick.AddListener(ClosePopupCategorySelected);
        //     // btn_Casual_Play.onClick.AddListener(PlayWithCategory);
        //     // btn_Casual_Continue.onClick.AddListener(ContinueWithCategory);
        //     // btn_Progress_Play_Next.onClick.AddListener(ShowProgressPlay);
        //     // btn_Progress_Level.onClick.AddListener(ShowProgressLevel);
        // }

    }
    public void ClosePopupCategorySelected()
    {
        PopupContainer.Instance.ClosePopup();
        Debug.Log("You have clicked the button!");
    }
    void OpenDifficultyContainer()
    {
        selectModeContainer.interactable = false;
        selectModeContainer.blocksRaycasts = false;
        selectModeContainer.alpha = 0f;

        SelectDifficultyContainer.interactable = true;
        SelectDifficultyContainer.blocksRaycasts = true;
        SelectDifficultyContainer.alpha = 1f;
    }
    void OpenModeContainer()
    {
        selectModeContainer.interactable = true;
        selectModeContainer.blocksRaycasts = true;
        selectModeContainer.alpha = 1f;

        SelectDifficultyContainer.interactable = false;
        SelectDifficultyContainer.blocksRaycasts = false;
        SelectDifficultyContainer.alpha = 0f;
    }
    public void PlayWithCategory()
    {
        Debug.Log("PlayWithCategory");
        OpenDifficultyContainer();
    }
    public void ContinueWithCategory()
    {
        Debug.Log("ContinueWithCategory");
        // OpenDifficultyContainer();
    }
    public void PlayNextLevelProgress()
    {
        Debug.Log("PlayNextLevelProgress");
        CategoryInfo activeCategory = GameManager.Instance.ActiveCategoryInfo;
        int activeLevel = GameManager.Instance.GetLastCompletedLevels()[activeCategory.saveId];
        TextAsset levelFile = activeCategory.levelFiles[activeLevel];
    }
    public void ShowProgressLevel()
    {
        ClosePopupCategorySelected();
        OpenModeContainer();
        ScreenManager.Instance.ShowScreenLevel(GameManager.Instance.ActiveCategoryInfo);
    }

    public void OnDifficultySelected(int difficultyIndex)
    {
        switch (difficultyIndex)
        {
            case 0:
                Debug.Log("EasyButton");
                break;
            case 1:
                Debug.Log("MediumButton");
                break;
            case 2:
                Debug.Log("HardButton");
                break;
        }
    }
}
