using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupContainer : SingletonComponent<PopupContainer>
{
    [SerializeField] private GameObject levelCompletePopup = null;
    [SerializeField] private GameObject categorySelectedPopup = null;
    [SerializeField] private GameObject chooseHighlighLetterPopup = null;
    [SerializeField] private GameObject settingsPopup = null;
    [SerializeField] private GameObject unlockCategoryPopup = null;
    [SerializeField] private GameObject notEnoughCoinsPopup = null;
    [SerializeField] private GameObject notEnoughKeysPopup = null;
    [SerializeField] private GameObject rewardAdGranted = null;
    [SerializeField] private GameObject storePopup = null;
    [SerializeField] private GameObject background = null;
    // Start is called before the first frame update
    private Vector3 scaleChange = new Vector3(-0.5f, -0.5f, -0.5f);
    public void ShowCategorySelectedPopup()
    {
        background.SetActive(true);
        // Debug.Log(categorySelectedPopup);
        // Debug.Log(categorySelectedPopup.transform);
        categorySelectedPopup.transform.localScale += scaleChange;
        categorySelectedPopup.SetActive(true);
        categorySelectedPopup.transform.DOScale(Vector3.one, 0.5f)
        .SetEase(Ease.OutBack);

    }


    public void ClosePopup()
    {
        background.SetActive(false);
        levelCompletePopup.SetActive(false);
        categorySelectedPopup.SetActive(false);
        chooseHighlighLetterPopup.SetActive(false);
        settingsPopup.SetActive(false);
        unlockCategoryPopup.SetActive(false);
        notEnoughCoinsPopup.SetActive(false);
        notEnoughKeysPopup.SetActive(false);
        rewardAdGranted.SetActive(false);
        storePopup.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
