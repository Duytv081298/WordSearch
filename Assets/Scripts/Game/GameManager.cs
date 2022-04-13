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
    public int Coins { get; set; }
    public int Keys { get; set; }
    public CategoryInfo ActiveCategoryInfo { get; set; }
    public Board ActiveBoard { get; private set; }
    public GameMode ActiveGameMode { get; private set; }
    public GameState ActiveGameState { get; private set; }
    public Dictionary<string, int> LastCompletedLevels = null;

    void Awake()
    {

        LastCompletedLevels = new Dictionary<string, int>();

        characterGrid.Initialize();
        wordListContainer.Initialize();
    }

    void Start()
    {
        // SaveableManager.Instance.LoadSaveData(categoryInfos);
        screenManager.Initialize(categoryInfos);
        // Debug.Log(lastCompletedLevels["birds"]);

        // LevelArray test = new LevelArray();
        // SaveableManager.Instance.SetString("key1" ,test.SaveToString());
        // string testtr = SaveableManager.Instance.GetString("key1");
        // Debug.Log(testtr);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Board BoardFromJson(CategoryInfo categoryInfo, int level)
    {
        TextAsset levelFile = categoryInfo.levelFiles[level];
        Debug.Log(levelFile);
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

        // Đảo ngược chuỗi text
        for (int i = 0; i < selectedWord.Length; i++)
        {
            char character = selectedWord[i];

            selectedWordReversed = character + selectedWordReversed;
        }

        // Check if the selected word equals any of the hidden words
        for (int i = 0; i < ActiveBoard.words.Count; i++)
        {
            string word = ActiveBoard.words[i];

            // Kiểm tra từ đã được tìm thấy chưa
            if (ActiveBoard.foundWords.Contains(word))
            {
                continue;
            }

            // Loại bỏ khoảng trắng
            string wordNoSpaces = word.Replace(" ", "");

            // kiểm tra sự trùng khớp
            if (selectedWord == wordNoSpaces || selectedWordReversed == wordNoSpaces)
            {
                // Thêm vào danh sách từ đã tìm thấy
                ActiveBoard.foundWords.Add(word);

                // Thông báo cho wordListContainer tiến hành đánh dấu word đã được chọn
                wordListContainer.SetWordFound(word);

                //kiểm tra đã tìm đủ word chưa
                if (ActiveBoard.foundWords.Count == ActiveBoard.words.Count)
                {
                    // BoardCompleted();
                    Debug.Log("Thắng");
                }

                // Return the word with the spaces
                return word;
            }
        }

        return null;
    }


    public void HintHighlightWord()
    {

        // Coins = 1000;
        if (ActiveBoard == null)
        {
            return;
        }

        List<string> nonFoundWords = new List<string>();

        // Lấy ra các từ chưa được tìm thấy
        for (int i = 0; i < ActiveBoard.words.Count; i++)
        {
            string word = ActiveBoard.words[i];

            if (!ActiveBoard.foundWords.Contains(word))
            {
                nonFoundWords.Add(word);
            }
        }

        // Đảm bảo danh dách không âm
        if (nonFoundWords.Count == 0)
        {
            Debug.Log("nonFoundWords.Count = 0 ");
            return;
        }

        // Check lượng coins Player có
        if (Coins < coinCostWordHint)
        {
            Debug.Log("Coins < coinCostWordHint");
            // Show the not enough coins popup
            // PopupManager.Instance.Show("not_enough_coins");
            PopupContainer.Instance.ShowNotEnoughCoinsPopup();
        }
        else
        {
            // Pick a random word to show
            string wordToShow = nonFoundWords[Random.Range(0, nonFoundWords.Count)];

            // Set it as selected
            OnWordSelected(wordToShow);

            // Highlight the word
            characterGrid.ShowWordHint(wordToShow);

            // Deduct the cost
            Coins -= coinCostWordHint;

            // SoundManager.Instance.Play("hint-used");
        }
    }

    public void HintHighlightLetter()
    {

    }
















    //trả về true nếu level completed
    public bool IsLevelCompleted(CategoryInfo categoryInfo, int levelIndex)
    {
        return LastCompletedLevels.ContainsKey(categoryInfo.saveId) && levelIndex <= LastCompletedLevels[categoryInfo.saveId];
    }

    // trả về true nếu level đang bị khóa
    public bool IsLevelLocked(CategoryInfo categoryInfo, int levelIndex)
    {
        return levelIndex > 0 && (!LastCompletedLevels.ContainsKey(categoryInfo.saveId) || levelIndex > LastCompletedLevels[categoryInfo.saveId] + 1);
    }

}
