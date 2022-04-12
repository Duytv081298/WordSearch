using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenManager : SingletonComponent<ScreenManager>
{
    [SerializeField]
    private GameObject mainScreen = null;
    [SerializeField]
    private GameObject gameScreen = null;
    [SerializeField]
    private GameObject levelScreen = null;
    private List<string> backStack;
    // The screen that is currently being shown
    private GameObject currentScreen;
    // Start is called before the first frame update


    [SerializeField] private List<CategoryInfo> categoryInfos = null;

    public void Initialize(List<CategoryInfo> categoryInfos)
    {
        MainScreen _mainScreenScript = mainScreen.GetComponent<MainScreen>();
        _mainScreenScript.Initialize(categoryInfos);
    }


    public void ChangeGameScreen()
    {
        // gameScreen.SetActiveScreen(true);
        // mainScreen.SetActiveScreen(false);
        // var _renderer = mainScreen.GetRenderer();
        // // var _color = _renderer.material.color;
        // Debug.Log(_renderer);
        // // Debug.Log(JsonUtility.ToJson(_color));
        // // Debug.Log(_color);
        // Tween myTween = mainScreen.GetTransform().DOMoveX(-1080 / 2, 1);
        // // mainScreen.GetGameObject().DOFade(-1080/2, 1);
    }
    public void ChangeMainScreen()
    {
        // mainScreen.SetActiveScreen(true);
        // gameScreen.SetActiveScreen(false);
    }

    public void OpenMainScreen()
    {

    }
    public void OpenGameScreen(TextAsset levelFile)
    {
        // levelScreen.SetActiveScreen(false);
        // gameScreen.SetActiveScreen(true);
    }
    public void OpenlevelScreen()
    {

        // levelScreen.SetActiveScreen(true);
        // mainScreen.SetActiveScreen(false);
    }
    public void ShowScreen(string idScreen)
    {
        HideCurrentScreen();
        GameObject screenActive = GetScreenById(idScreen);
        SetVisibility(screenActive, true);
        currentScreen = screenActive;
        backStack.Add(idScreen);
    }
    public void ShowScreenMain()
    {
        if (currentScreen) SetVisibility(currentScreen, false);
        SetVisibility(mainScreen, true);
        currentScreen = mainScreen;
        backStack.Add("main");
    }
    public void ShowScreenGame()
    {
        if (currentScreen) SetVisibility(currentScreen, false);
        SetVisibility(gameScreen, true);
        currentScreen = gameObject;
        backStack.Add("game");
        
    }

    public void ShowScreenLevel(CategoryInfo ActiveCategoryInfo)
    {
        if (currentScreen) SetVisibility(currentScreen, false);
        SetVisibility(levelScreen, true);
        currentScreen = levelScreen;
        backStack.Add("levels");

        LevelScreen _levelScript = levelScreen.GetComponent<LevelScreen>();
        _levelScript.Initialize(ActiveCategoryInfo);
    }
    private void SetVisibility(GameObject screen, bool isVisible)
    {
        CanvasGroup screenCG = screen.GetComponent<CanvasGroup>();
        screenCG.alpha = isVisible ? 1f : 0f;
        screenCG.interactable = isVisible ? true : false;
        screenCG.blocksRaycasts = isVisible ? true : false;
    }

    void HideCurrentScreen()
    {
        if (currentScreen) currentScreen.SetActive(false);
    }
    GameObject GetScreenById(string id)
    {
        GameObject result = null;
        switch (id)
        {
            case "main":
                result = mainScreen;
                break;
            case "levels":
                result = levelScreen;
                break;
            case "game":
                result = gameScreen;
                break;
        }
        return result;
    }

    void Start()
    {
        backStack = new List<string>();
        ShowScreenMain();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
