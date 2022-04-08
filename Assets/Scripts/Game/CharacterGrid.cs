using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CharacterGrid : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called before the first frame update


    [SerializeField] private float maxCellSize = 0;
    [SerializeField] private GameObject characterGridItemPrefab = null;
    [SerializeField] private RectTransform grid_container = null;

    private List<List<char>> boardCharacters = null;
    private List<List<CharacterGridItem>> characterItems;



    private float currentScale;
    private float currentCellSize;



    private bool isSelecting;
    private int selectingPointerId;
    private CharacterGridItem startCharacter;
    private CharacterGridItem lastEndCharacter;




    private float CellFullWidth { get { return currentCellSize; } }
    private float CellFullHeight { get { return currentCellSize; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectingPointerId != -1)
        {
            // There is already a mouse/pointer highlighting words 
            return;
        }

        Debug.Log("OnPointerDown: ");
        CharacterGridItem characterItem = GetCharacterItemAtPosition(eventData.position);
        isSelecting = true;
        selectingPointerId = eventData.pointerId;
        startCharacter = characterItem;
        lastEndCharacter = characterItem;
        Debug.Log("Row: " + characterItem.Row + "  Col: " + characterItem.Col);
        // Debug.Log(characterItem.Col);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag: ");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp: ");
    }
    void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        characterItems = new List<List<CharacterGridItem>>();
        LevelInfo activeLevel = GameManager.Instance.GetActiveLevel();

        currentCellSize = SetupGridContainer(activeLevel.rows, activeLevel.cols);
        currentScale = currentCellSize / maxCellSize;
        Debug.Log(currentScale);
        // Debug.Log("CharacterGrid");
        boardCharacters = activeLevel.boardCharacters;
        for (int i = 0; i < boardCharacters.Count; i++)
        {
            characterItems.Add(new List<CharacterGridItem>());
            for (int j = 0; j < boardCharacters[i].Count; j++)
            {
                char text = boardCharacters[i][j];
                GameObject _characterItem = Instantiate(characterGridItemPrefab, Vector3.zero, Quaternion.identity, grid_container);
                CharacterGridItem _characterItemScript = _characterItem.GetComponent<CharacterGridItem>();
                _characterItemScript.Setup(text, new Vector3(currentScale, currentScale, 1f));

                _characterItemScript.Row = i;
                _characterItemScript.Col = j;
                _characterItemScript.IsHighlighted = false;

                characterItems[i].Add(_characterItemScript);
            }
        }
        selectingPointerId = -1;
    }
    private float SetupGridContainer(int rows, int columns)
    {
        // Add a GridLayoutGroup so make positioning letters much easier
        GridLayoutGroup gridLayoutGroup = grid_container.GetComponent<GridLayoutGroup>();

        // Get the width and height of a cell
        float cellWidth = grid_container.rect.width / (float)columns;
        float cellHeight = grid_container.rect.height / (float)rows;
        float cellSize = Mathf.Min(cellWidth, cellHeight, maxCellSize);

        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
        return cellSize;
    }

    private CharacterGridItem GetCharacterItemAtPosition(Vector2 screenPoint)
    {
        for (int i = 0; i < characterItems.Count; i++)
        {
            for (int j = 0; j < characterItems[i].Count; j++)
            {
                Vector2 localPoint;

                RectTransformUtility.ScreenPointToLocalPointInRectangle(characterItems[i][j].transform as RectTransform, screenPoint, null, out localPoint);

                // Check if the localPoint is inside the cell in the grid
                localPoint.x += CellFullWidth / 2f;
                localPoint.y += CellFullHeight / 2f;

                if (localPoint.x >= 0 && localPoint.y >= 0 && localPoint.x < CellFullWidth && localPoint.y < CellFullHeight)
                {
                    return characterItems[i][j];
                }
            }
        }

        return null;
    }

}
