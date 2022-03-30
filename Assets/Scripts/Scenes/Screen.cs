using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private string screenId = null;
    [SerializeField] private string nameScreen = null;

    // Start is called before the first frame update

    public void Initialize()
    {

    }
    public GameObject GetGameObject()
    {
        Debug.Log(gameObject.name);
        return gameObject;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
