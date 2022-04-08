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

        // Debug.Log();
        this.categoryInfos = categoryInfos;
        // MainScreen _mainScreenScript = mainScreen.GetComponent<MainScreen>();
        // Debug.Log(JsonUtility.ToJson(GameManager.Instance.GetCategoryInfos()));
        // _mainScreenScript.Initialize(categoryInfos);
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
        screenActive.SetActive(true);
        currentScreen = screenActive;
        backStack.Add(idScreen);
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
        ShowScreen("main");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
