using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordListItem : MonoBehaviour
{
    [SerializeField] private Text wordText = null;
    [SerializeField] private GameObject foundIndicator = null;

    public void Setup(string word)
    {
        wordText.text = word;
        foundIndicator.SetActive(false);
    }

    public void SetWordFound()
    {
        foundIndicator.SetActive(true);
    }

}
