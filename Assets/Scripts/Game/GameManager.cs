using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
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

    [Header("Components")]
    [SerializeField] private ScreenManager screenManager = null;

    [SerializeField] private CategoryInfo activeCategoryInfo = null;
    [SerializeField] private LevelInfo activeLevel = null;

  public List<CategoryInfo> GetCategoryInfos()
    {
        return this.categoryInfos;
    }

    public void SetCategoryInfos(List<CategoryInfo> categoryInfos)
    {
        this.categoryInfos = categoryInfos;
    }
    public LevelInfo GetActiveLevel()
    {
        return this.activeLevel;
    }

    public void SetActiveLevel(LevelInfo activeLevel)
    {
        this.activeLevel = activeLevel;
    }

    public Dictionary<string, int> lastCompletedLevels = null;

    public Dictionary<string, int> GetLastCompletedLevels()
    {
        return this.lastCompletedLevels;
    }

    public void SetLastCompletedLevels(Dictionary<string, int> lastCompletedLevels)
    {
        this.lastCompletedLevels = lastCompletedLevels;
    }


    public CategoryInfo GetActiveCategory()
    {
        return this.activeCategoryInfo;
    }

    public void SetActiveCategory(CategoryInfo category)
    {
        this.activeCategoryInfo = category;
    }

    // [SerializeField] private CharacterGrid characterGrid = null;
    // [SerializeField] private WordList wordList = null;
    // [SerializeField] private GameObject loadingIndicator = null;

    // public Dictionary<string, Board> BoardsInProgress { get; private set; }
    // public Dictionary<string, int> LastCompletedLevels { get; private set; }
    // public Dictionary<string, JSONNode> SavedBoards { get; private set; }
    // public HashSet<string> UnlockedCategories { get; private set; }

    public int Coins { get; set; }
    public int Keys { get; set; }
    void Awake()
    {

        lastCompletedLevels = new Dictionary<string, int>();
        // Debug.Log(JsonUtility.ToJson(categoryInfos[0]));
    }

    void Start()
    {
        // screenManager.Initialize(categoryInfos);
        SaveableManager.Instance.LoadSaveData(categoryInfos);
        // Debug.Log(lastCompletedLevels["birds"]);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
