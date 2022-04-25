using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;
public class LevelScreen : MonoBehaviour, IRecyclableScrollRectDataSource
{
    // Start is called before the first frame update
    // [SerializeField] private GameObject levelItemPrefab = null;
    // [SerializeField] private RectTransform levlelListContainer = null;
    [SerializeField] private TopBar topBar = null;
    [SerializeField] private string id = "levels";
    [SerializeField] RecyclableScrollRect _recyclableScrollRect;
    private List<TextAsset> _contactList = new List<TextAsset>();
    private void Awake()
    {
        _recyclableScrollRect.DataSource = this;
    }
    public void Initialize(CategoryInfo activeCategoryInfo)
    {
        _contactList = activeCategoryInfo.levelFiles;
        _recyclableScrollRect.ReloadData();
        topBar.SetCategoryName(activeCategoryInfo.displayName);

    }
    public int GetItemCount()
    {
        return _contactList.Count;
    }
    public void SetCell(ICell cell, int index)
    {
        var item = cell as LevelListItem;
        item.Initialize(_contactList[index], index);
    }
    public void ReloadData()
    {
        _recyclableScrollRect.ReloadData();
    }
}
