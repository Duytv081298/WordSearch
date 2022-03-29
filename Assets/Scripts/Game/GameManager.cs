using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private string characters = null;
    [SerializeField] private List<CategoryInfo> categoryInfos = null;
    [SerializeField] private List<DifficultyInfo> difficultyInfos = null;

    [Header("Values")]
    [SerializeField] private int startingCoins = 0;
    [SerializeField] private int startingKeys = 0;
    [SerializeField] private int numLevelsToAwardCoins = 0;
    [SerializeField] private int coinsToAward = 0;
    [SerializeField] private int coinCostWordHint = 0;
    [SerializeField] private int coinCostLetterHint = 0;




    // public Dictionary<string, Board> BoardsInProgress { get; private set; }
    // public Dictionary<string, int> LastCompletedLevels { get; private set; }
    // public Dictionary<string, JSONNode> SavedBoards { get; private set; }
    // public HashSet<string> UnlockedCategories { get; private set; }

    public int Coins { get; set; }
    public int Keys { get; set; }
    void Awake()
    {
        
        Debug.Log(JsonUtility.ToJson(categoryInfos[0]));
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
