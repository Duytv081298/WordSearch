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
        for (int i = 0; i < 3; i++)
        {
            var category = categoryInfos[i];
            Debug.Log(JsonUtility.ToJson(category));
            // categoryListItemPrefab.SpawbCategoryItem(category, categoryListContainer);

            GameObject _categoryItem = Instantiate(categoryItemPrefab, Vector3.zero, Quaternion.identity, categoryListContainer);
            CategoryListItem _categoryScript = _categoryItem.GetComponent<CategoryListItem>();
            _categoryScript.setText(category.displayName);
            Debug.Log(_categoryScript.getText());
            // Debug.Log(_categoryScript);
        }
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    void Start()
    { }

    // Update is called once per frame
    void Update()
    {

    }
}
