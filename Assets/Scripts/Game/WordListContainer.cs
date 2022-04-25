using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordListContainer : MonoBehaviour
{
    [SerializeField] private GameObject wordListItemPrefab = null;

    [SerializeField] private RectTransform wordListContainer = null;


    private Dictionary<string, WordListItem> wordListItems;

    // Start is called before the first frame update


    public void Initialize()
    {
        wordListItems = new Dictionary<string, WordListItem>();
    }
    public void Setup(Board board)
    {
        Clear();
        // Debug.Log("WordListContainer Setup");
        foreach (var word in board.words)
        {

            CreateWordListItem(word);
        }
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
    private WordListItem CreateWordListItem(string word)
    {
        WordListItem _wordItemScript = null;

        if (!wordListItems.ContainsKey(word))
        {
            GameObject _wordItem = Instantiate(wordListItemPrefab, Vector3.zero, Quaternion.identity, wordListContainer);
            _wordItemScript = _wordItem.GetComponent<WordListItem>();
            _wordItemScript.Setup(word);

            wordListItems.Add(word, _wordItemScript);
        }
        else
        {
            Debug.LogWarning("[WordList] Board contains duplicate words. Word: " + word);
        }

        return _wordItemScript;
    }
    // Update is called once per frame
    public void Clear()
    {
        foreach (KeyValuePair<string, WordListItem> item in wordListItems)
        {
            Destroy(item.Value.gameObject);
        }
        wordListItems.Clear();

    }
}
