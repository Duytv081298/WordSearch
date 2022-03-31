using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private CategoryListItem categoryListItemPrefab = null;
    [SerializeField] private GameObject categoryItemPrefab = null;
    [SerializeField] private RectTransform categoryListContainer = null;
    [SerializeField] private ScrollRect categoryListScrollRect = null;
    [SerializeField] private List<CategoryInfo> categoryInfos = null;

    // [Space]
    // private RecyclableListHandler<CategoryInfo>	categoryListHandler;
    // 	private CategoryInfo						selectedCategory;

    public void Initialize(List<CategoryInfo> categoryInfos)
    {
        this.categoryInfos = categoryInfos;
        for (int i = 0; i < categoryInfos.Count; i++)
        {
            var category = categoryInfos[i];
            GameObject _categoryItem = Instantiate(categoryItemPrefab, Vector3.zero, Quaternion.identity, categoryListContainer);
            CategoryListItem _categoryScript = _categoryItem.GetComponent<CategoryListItem>();
            _categoryScript.Initialize(category);
        }
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public Transform GetTransform()
    {
        return transform;
    }

    void Start()
    {}

    // Update is called once per frame
    void Update()
    {

    }
}
