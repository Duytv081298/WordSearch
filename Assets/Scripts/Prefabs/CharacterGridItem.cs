using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterGridItem : MonoBehaviour
{
    public Text characterText;
    public int Row { get; set; }
    public int Col { get; set; }
    public bool IsHighlighted { get; set; }

    public void Setup(char text, Vector3 scale, Vector2 scaledLetterOffsetInCell)
    {
        // Debug.Log(text);
        characterText.text = text.ToString();
        characterText.transform.localScale = scale;
        (transform as RectTransform).anchoredPosition = scaledLetterOffsetInCell;
    }
}
