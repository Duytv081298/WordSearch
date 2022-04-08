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

    public void Setup(char text, Vector3 scale)
    {
        // Debug.Log(text);
        characterText.text = text.ToString();
        characterText.transform.localScale = scale;
    }
      public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(22222222);
        Debug.Log(this.gameObject.name + " Was Clicked.");
    }
}
