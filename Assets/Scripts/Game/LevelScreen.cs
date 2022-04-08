using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject levelItemPrefab = null;
    [SerializeField] private RectTransform levlelListContainer = null;
    [SerializeField] private string id = "levels";

    void Start()
    {
        List<TextAsset> levelFiles = GameManager.Instance.GetActiveCategory().levelFiles;
        for (int i = 0; i < levelFiles.Count; i++)
        {
            TextAsset levelFile = levelFiles[i];
            GameObject _levelItem = Instantiate(levelItemPrefab, Vector3.zero, Quaternion.identity, levlelListContainer);
            LevelListItem _levelScript = _levelItem.GetComponent<LevelListItem>();
            _levelScript.Initialize(levelFile, i);
        }
        // Debug.Log(JsonUtility.ToJson(GameManager.Instance.GetActiveCategory()));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
