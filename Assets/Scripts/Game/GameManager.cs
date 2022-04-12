using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    public enum GameMode
    {
        Casual,
        Progress
    }
    public enum GameState
    {
        None,
        GeneratingBoard,
        BoardActive
    }
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
    [SerializeField] private CharacterGrid characterGrid = null;
    [SerializeField] private WordListContainer wordListContainer = null;
    [SerializeField] private ScreenManager screenManager = null;

    public CategoryInfo ActiveCategoryInfo { get; private set; }

    public Board ActiveBoard { get; private set; }


    public GameMode ActiveGameMode { get; private set; }
    public GameState ActiveGameState { get; private set; }
    public void SetActiveCategory(int idCategory)
    {
        ActiveCategoryInfo = categoryInfos[idCategory];
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

        characterGrid.Initialize();
        wordListContainer.Initialize();
        // Debug.Log(JsonUtility.ToJson(categoryInfos[0]));
    }

    void Start()
    {
        SaveableManager.Instance.LoadSaveData(categoryInfos);
        screenManager.Initialize(categoryInfos);
        // Debug.Log(lastCompletedLevels["birds"]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Board BoardFromJson(CategoryInfo categoryInfo, int level)
    {
        TextAsset levelFile = categoryInfo.levelFiles[level];
        Board board = new Board();
        board.FromJson(levelFile);
        // Debug.Log(board.foundWords);
        return board;
    }

    public void StartLevel(CategoryInfo categoryInfo, int levelIndex)
    {

        ActiveBoard = BoardFromJson(categoryInfo, levelIndex);

        SetupGame(ActiveBoard);

        ScreenManager.Instance.ShowScreenGame();

    }
    private void SetupGame(Board ActiveBoard)
    {
        characterGrid.SetUp(ActiveBoard);
        wordListContainer.Setup(ActiveBoard);


        ActiveGameState = GameState.BoardActive;
    }

    public string OnWordSelected(string selectedWord)
    {
        string selectedWordReversed = "";

        // Get the reverse version of the word
        for (int i = 0; i < selectedWord.Length; i++)
        {
            char character = selectedWord[i];

            selectedWordReversed = character + selectedWordReversed;
        }

        // Check if the selected word equals any of the hidden words
        for (int i = 0; i < ActiveBoard.words.Count; i++)
        {
            // Get the word and the word with no spaces without spaces
            string word = ActiveBoard.words[i];

            // Check if the word we has already been found
            if (ActiveBoard.foundWords.Contains(word))
            {
                continue;
            }

            // Spaces are removed from the word before being places on the board so we need to compare the word without any spaces in it
            string wordNoSpaces = word.Replace(" ", "");

            // Check if the word matches the selected word or the selected word reversed
            if (selectedWord == wordNoSpaces || selectedWordReversed == wordNoSpaces)
            {
                // Add the word to the hash set of found words for this board
                ActiveBoard.foundWords.Add(word);

                Debug.Log(word);
                // Notify the word list a word has been found
                wordListContainer.SetWordFound(word);

                if (ActiveBoard.foundWords.Count == ActiveBoard.words.Count)
                {
                    // BoardCompleted();
                }

                // Return the word with the spaces
                return word;
            }
        }

        return null;
    }

}
