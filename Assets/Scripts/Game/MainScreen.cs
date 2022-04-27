using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using PolyAndCode.UI;
// public class MainScreen : MonoBehaviour, IRecyclableScrollRectDataSource
public class MainScreen : MonoBehaviour
{
    // [SerializeField] private string id = "main";

    // [SerializeField] RecyclableScrollRect _recyclableScrollRect;

    // [SerializeField] private List<CategoryInfo> categoryInfos = null;


    // private void Awake()
    // {
    //     _recyclableScrollRect.DataSource = this;
    // }
    // void Start()
    // {

    // }
    // public int GetItemCount()
    // {
    //     return categoryInfos.Count;
    // }
    // public void SetCell(ICell cell, int index)
    // {
    //     var item = cell as CategoryScripts;
    //     item.Setup(categoryInfos[index], index);
    // }

    // public void Initialize(List<CategoryInfo> categoryInfos)
    // {

    //     this.categoryInfos = categoryInfos;
    //     _recyclableScrollRect.ReloadData();
    // }

    // public void ReloadData()
    // {
    //     _recyclableScrollRect.ReloadData();
    // }




    [Space]

    [SerializeField] private ScrollRect scrollRect = null;
    [SerializeField] private RectTransform content = null;
    [SerializeField] private TestScriptCategory itemCategory = null;
    [SerializeField] private ListLevelTest itemLevel = null;
    [SerializeField] private float expandAnimDuration = 0.5f;



    private ExpandableListHandler<CategoryInfo> expandableListHandler;
    private ObjectPool levelListItemPool;
    // private CategoryInfo selectedCategory;


    public void Initialize(List<CategoryInfo> categoryInfos)
    {
        // Tạo Pool Container lưu trữ các item level chưa sử dụng đến CreatePoolContainer 
        // CreatePoolContainer trả về 1 transform
        // truyền data vào ObjectPool, tạo ra 1 PoolObject bằng Hàm CreateObject

        levelListItemPool = new ObjectPool(itemLevel.gameObject, 1, ObjectPool.CreatePoolContainer(transform));
        expandableListHandler = new ExpandableListHandler<CategoryInfo>(categoryInfos, itemCategory, content, scrollRect, expandAnimDuration);

        // Add a listener for when a PackListItem is first created to pass it the level list item pool
        expandableListHandler.OnItemCreated += (ExpandableListItem<CategoryInfo> categoryInfo) =>
        {
            (categoryInfo as TestScriptCategory).SetLevelListItemPool(levelListItemPool);
        };

        expandableListHandler.Setup();
    }

    public void ReloadData()
    {
        expandableListHandler.Refresh();
    }


}
