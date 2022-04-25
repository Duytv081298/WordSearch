using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
public class MainScreen : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField] private string id = "main";

    [SerializeField] RecyclableScrollRect _recyclableScrollRect;

    [SerializeField] private List<CategoryInfo> categoryInfos = null;


    private void Awake()
    {
        _recyclableScrollRect.DataSource = this;
    }
    void Start()
    {

    }
    public int GetItemCount()
    {
        return categoryInfos.Count;
    }
    public void SetCell(ICell cell, int index)
    {
        var item = cell as CategoryScripts;
        item.Setup(categoryInfos[index], index);
    }

    public void Initialize(List<CategoryInfo> categoryInfos)
    {

        this.categoryInfos = categoryInfos;
        _recyclableScrollRect.ReloadData();
    }

    public void ReloadData()
    {
        _recyclableScrollRect.ReloadData();
    }


}
