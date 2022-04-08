using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordListContainer : MonoBehaviour
{
    [SerializeField] private GameObject wordListItemPrefab = null;

    [SerializeField] private RectTransform wordListContainer = null;


    private Dictionary<string, WordListItem> wordListItems;
    public void Initialize()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WordListContainer");
        // wordListItemPool = new ObjectPool(wordListItemPrefab.gameObject, 10, wordListContainer);
        LevelInfo activeLevel = GameManager.Instance.GetActiveLevel();
        List<string> words = activeLevel.words;
        foreach (var word in words)
        {
            GameObject _wordItem = Instantiate(wordListItemPrefab, Vector3.zero, Quaternion.identity, wordListContainer);
            WordListItem _wordItemScript = _wordItem.GetComponent<WordListItem>();
            _wordItemScript.Setup(word);
        }
        // Debug.Log(activeLevel);
        // Debug.Log(activeLevel.text);
        // var test = JsonUtility.FromJson<LevelInfo>(activeLevel.ToString());
        wordListItems = new Dictionary<string, WordListItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
