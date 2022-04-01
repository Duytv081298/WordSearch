using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelectedPopup : MonoBehaviour
{

    [SerializeField] private Button btn_Close_X = null;
    [SerializeField] private Button btn_Close = null;
    [SerializeField] private Button btn_Casual_Play = null;
    [SerializeField] private Button btn_Casual_Continue = null;
    [SerializeField] private Button btn_Progress_Play_Next = null;
    [SerializeField] private Button btn_Progress_Level = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("click");
            btn_Close_X.onClick.AddListener(ClosePopupCategorySelected);
            btn_Close.onClick.AddListener(ClosePopupCategorySelected);
            btn_Casual_Play.onClick.AddListener(PlayWithCategory);
            btn_Casual_Continue.onClick.AddListener(ContinueWithCategory);
            btn_Progress_Play_Next.onClick.AddListener(ShowProgressPlay);
            btn_Progress_Level.onClick.AddListener(ShowProgressLevel);
        }
        
    }
    void ClosePopupCategorySelected()
    {
        PopupContainer.Instance.ClosePopup();
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
    void PlayWithCategory()
    {

    }
    void ContinueWithCategory()
    {

    }
    void ShowProgressPlay()
    {

    }
    void ShowProgressLevel()
    {

    }
}
