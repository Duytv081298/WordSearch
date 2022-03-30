using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : SingletonComponent<ScreenManager>
{
    [SerializeField]
    private MainScreen mainScreen = null;
    [SerializeField]
    private GameScreen gameScreen = null;
    private List<string> backStack;
    // The screen that is currently being shown
    private Screen currentScreen;
    // Start is called before the first frame update


    [SerializeField] private List<CategoryInfo> categoryInfos = null;

    public void Initialize(List<CategoryInfo> categoryInfos)
    {
        this.categoryInfos = categoryInfos;
        mainScreen.Initialize(categoryInfos);
    }
    

    public void ChangeGameScreen()
    {
        gameScreen.GetGameObject().SetActive(true);
        mainScreen.GetGameObject().SetActive(false);
    }
    void Start()
    {
        // screens[0].GetGameObject();
        // screens[0].SetActive(true);
        // screens[1].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
