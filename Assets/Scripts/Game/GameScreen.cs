using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text wordHintCostText = null;
    [SerializeField] private Text letterHintCostText = null;

    public GameObject GetGameObject(){
        return gameObject;
    }

    void Start()
    {
        // gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
