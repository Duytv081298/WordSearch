using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordListContainer : MonoBehaviour
{
    [SerializeField] private RectTransform wordListItemPrefab = null;

    [SerializeField] private RectTransform wordListContainer = null;


    private Dictionary<string, WordListItem> wordListItems;
    private List<RectTransform> rowWordLists = null;
    private List<string> listWordUse = null;

    // Start is called before the first frame update


    public void Initialize()
    {
        wordListItems = new Dictionary<string, WordListItem>();
        rowWordLists = new List<RectTransform>();
        listWordUse = new List<string>();
    }
    public void Setup(Board board)
    {
        Clear();
        // Debug.Log("WordListContainer Setup");
        board.ShuffleListString();
        foreach (var word in board.words)
        {
            CreateWordListItem(word);
        }
        Canvas.ForceUpdateCanvases();
        float totalWidthWordList = GetTotalWidthWordList();
        int row = (int)Mathf.Ceil(totalWidthWordList / wordListContainer.rect.width);
        CreateRowWordList(3);

        int count = wordListItems.Count;
        int phanDu = count % 3;
        int phanNguyen = count / 3;
        if (row < 3)
        {
            Debug.Log("count: " + count + "    phần Nguyên: " + phanNguyen + "   Phần dư: " + phanDu);
        }

        // int index = 0;
        // float widthWordList = 0f;
        // foreach (var item in wordListItems)
        // {
        //     WordListItem _wordItemScript = item.Value;
        //     RectTransform _wordItemRecT = _wordItemScript.GetComponent<RectTransform>();
        //     widthWordList += (_wordItemRecT.sizeDelta.x + 30f);
        //     if (widthWordList >= rowWordLists[index].rect.width)
        //     {
        //         index++;
        //         if (index > rowWordLists.Count - 1) CreateRowWordList(1);
        //         widthWordList = (_wordItemRecT.sizeDelta.x + 30f);
        //     }
        //     _wordItemScript.SetParent(rowWordLists[index]);
        //     _wordItemScript.SetAlpha(true);
        // }
        // RemoveEmptyRow(rowWordLists);

        // Debug.Log("row: " + rowWordLists.Count);
        // var VLG = wordListContainer.GetComponent<VerticalLayoutGroup>();
        // if (rowWordLists.Count == 2)
        // {
        //     VLG.padding.top = 65;
        //     VLG.padding.bottom = 20;
        // }
        // else if (rowWordLists.Count == 3)
        // {
        //     VLG.padding.top = 35;
        //     VLG.padding.bottom = 25;

        // }
        // else
        // {
        //     VLG.padding.top = 0;
        //     VLG.padding.bottom = 0;
        // }


    }

    public void SetWordFound(string word)
    {
        if (wordListItems.ContainsKey(word))
        {
            wordListItems[word].SetWordFound();
        }
        else
        {
            Debug.LogError("[WordList] Word does not exist in the word list: " + word);
        }
    }
    private void CreateWordListItem(string word)
    {

        if (!wordListItems.ContainsKey(word))
        {
            RectTransform _wordItem = Instantiate(wordListItemPrefab, Vector3.zero, Quaternion.identity, wordListContainer);
            // _wordItem.gameObject.SetActive(false);

            _wordItem.localPosition = Vector3.zero;

            WordListItem _wordItemScript = _wordItem.GetComponent<WordListItem>();
            _wordItemScript.Setup(word);
            _wordItemScript.SetAlpha(false);

            wordListItems.Add(word, _wordItemScript);
        }
        else
        {
            Debug.LogWarning("[WordList] Board contains duplicate words. Word: " + word);
        }

        // return _wordItemScript;
    }
    // Update is called once per frame
    public void Clear()
    {
        foreach (KeyValuePair<string, WordListItem> item in wordListItems)
        {
            Destroy(item.Value.gameObject);
        }
        wordListItems.Clear();
        foreach (var item in rowWordLists)
        {
            Destroy(item.gameObject);
        }

        rowWordLists.Clear();


    }

    private bool IsVisible(RectTransform placeholder)
    {
        RectTransform viewport = wordListContainer.GetComponent<RectTransform>();

        float placeholderTop = -placeholder.anchoredPosition.y - placeholder.rect.height / 2f;
        float placeholderbottom = -placeholder.anchoredPosition.y + placeholder.rect.height / 2f;

        float viewportTop = viewport.anchoredPosition.y;
        float viewportbottom = viewport.anchoredPosition.y + viewport.rect.height;
        Debug.Log("viewportTop: " + viewportTop + "   viewportbottom: " + viewportbottom + "  placeholder.rect.height: " + placeholder.rect.height);
        Debug.Log("placeholderTop: " + placeholderTop + "   placeholderbottom: " + placeholderbottom + "   viewport.rect.height: " + viewport.rect.height);

        return placeholderTop < viewportbottom && placeholderbottom > viewportTop;
    }

    private RectTransform CreateContainer(string name, params System.Type[] types)
    {
        GameObject containerObj = new GameObject(name, types);
        RectTransform container = containerObj.GetComponent<RectTransform>();

        container.SetParent(wordListContainer, false);
        container.anchoredPosition = Vector2.zero;
        container.anchorMin = Vector2.up;
        container.anchorMax = Vector2.one;
        container.offsetMin = Vector2.zero;
        container.offsetMax = Vector2.zero;
        container.sizeDelta = new Vector2(container.sizeDelta.x, 54f);


        return container;
    }
    private float GetTotalWidthWordList()
    {
        float totalWidthWordList = 0f;
        foreach (var item in wordListItems)
        {
            WordListItem _wordItemScript = item.Value;
            RectTransform _wordItemRecT = _wordItemScript.GetComponent<RectTransform>();
            totalWidthWordList += (_wordItemRecT.sizeDelta.x + 100f);
        }
        return totalWidthWordList;
    }
    private void CreateRowWordList(int row)
    {
        Debug.Log("row: " + row);
        for (int i = 0; i < row; i++)
        {
            RectTransform rowWordList = CreateContainer("Row Word List", typeof(RectTransform));
            HorizontalLayoutGroup horizontalLayoutGroup = rowWordList.gameObject.AddComponent<HorizontalLayoutGroup>();
            horizontalLayoutGroup.spacing = 50f;
            rowWordLists.Add(rowWordList);
        }
    }
    private void RemoveEmptyRow(List<RectTransform> listRows)
    {
        for (int i = 0; i < listRows.Count; i++)
        {
            var row = listRows[i];
            if (row.childCount == 0)
            {
                Destroy(row.gameObject);
                listRows.RemoveAt(i);
            }
        }

    }

}
