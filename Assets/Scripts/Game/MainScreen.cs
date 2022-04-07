using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private GameObject categoryItemPrefab = null;
    [SerializeField] private RectTransform categoryListContainer = null;
    [SerializeField] private List<CategoryInfo> categoryInfos = null;
    [SerializeField] private string id = "main";

    // [Space]
    // private RecyclableListHandler<CategoryInfo>	categoryListHandler;
    // 	private CategoryInfo						selectedCategory;

    void Start()
    { 
        this.categoryInfos = GameManager.Instance.GetCategoryInfos();
        for (int i = 0; i < categoryInfos.Count; i++)
        {
            var category = categoryInfos[i];
            GameObject _categoryItem = Instantiate(categoryItemPrefab, Vector3.zero, Quaternion.identity, categoryListContainer);
            CategoryListItem _categoryScript = _categoryItem.GetComponent<CategoryListItem>();
            _categoryScript.Initialize(category);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
