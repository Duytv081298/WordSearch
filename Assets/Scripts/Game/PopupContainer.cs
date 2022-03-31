using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopupContainer : SingletonComponent<PopupContainer>
{
    [SerializeField] private GameObject categorySelectedPopup = null;
    [SerializeField] private GameObject purdah = null;
    // Start is called before the first frame update
    private Vector3 scaleChange = new Vector3(-0.5f, -0.5f, -0.5f);
    public void ShowCategorySelectedPopup()
    {
        purdah.SetActive(true);
        // Debug.Log(categorySelectedPopup);
        // Debug.Log(categorySelectedPopup.transform);
        categorySelectedPopup.transform.localScale += scaleChange;
        categorySelectedPopup.SetActive(true);
        categorySelectedPopup.transform.DOScale(Vector3.one, 0.5f)
        .SetEase(Ease.OutBack);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
