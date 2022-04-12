using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text wordHintCostText = null;
    [SerializeField] private Text letterHintCostText = null;





    [Header("Components")]
    // [SerializeField] private CharacterGrid characterGrid = null;
    [SerializeField] private WordListContainer wordList = null;
    [SerializeField] private CharacterGrid CharacterGrid = null;

    [SerializeField] private string id = "game";

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    void Start()
    {
        Debug.Log("start game screen");
        //  Debug.Log(GameManager.Instance.GetActiveLevel());
        // gameObject.SetActive(true);
        // Debug.Log(GameManager.Instance.ActiveBoard);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        
    }
}
