using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CharacterGrid : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called before the first frame update

    private enum HighlighPosition
    {
        AboveLetters,
        BelowLetters
    }

    [SerializeField] private float maxCellSize = 200;
    [SerializeField] private SelectedWord selectedWord = null;

    [Header("Letter Settings")]
    [SerializeField] private Font letterFont = null;
    [SerializeField] private int letterFontSize = 150;
    [SerializeField] private Color letterColor = Color.black;
    [SerializeField] private Color letterHighlightedColor = Color.white;
    [SerializeField] private Vector2 letterOffsetInCell = Vector2.zero;

    [Header("Highlight Settings")]
    [SerializeField] private HighlighPosition highlightPosition = HighlighPosition.AboveLetters;
    [SerializeField] private Sprite highlightSprite = null;
    [SerializeField] private float highlightExtraSize = -35f;
    [SerializeField] private List<Color> highlightColors = null;



    [Header("Highlight Letter Settings")]
    [SerializeField] private Sprite highlightLetterSprite = null;
    [SerializeField] private float highlightLetterSize = 0f;
    [SerializeField] private Color highlightLetterColor = Color.white;

    private Image selectingHighlight;

    [SerializeField] private GameObject characterGridItemPrefab = null;

    private Board currentBoard;

    [Header("Container")]
    private RectTransform gridContainer;
    private RectTransform gridOverlayContainer;
    private RectTransform gridUnderlayContainer;
    private RectTransform highlighLetterContainer;

    private List<List<char>> boardCharacters = null;
    private List<List<CharacterGridItem>> characterItems;
    private List<Image> highlights;



    private float currentScale;
    private float currentCellSize;



    private bool isSelecting;
    private int selectingPointerId;
    private CharacterGridItem startCharacter;
    private CharacterGridItem lastEndCharacter;


    private float ScaledHighlighExtraSize { get { return highlightExtraSize * currentScale; } }
    private Vector2 ScaledLetterOffsetInCell { get { return letterOffsetInCell * currentScale; } }
    private float ScaledHightlightLetterSize { get { return highlightLetterSize * currentScale; } }
    private float CellFullWidth { get { return currentCellSize; } }
    private float CellFullHeight { get { return currentCellSize; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectingPointerId != -1)
        {
            // There is already a mouse/pointer highlighting words 
            return;
        }
        if (GameManager.Instance.ActiveGameState == GameManager.GameState.BoardActive)
        {
            CharacterGridItem characterItem = GetCharacterItemAtPosition(eventData.position);
            if (characterItem != null)
            {
                isSelecting = true;
                selectingPointerId = eventData.pointerId;
                startCharacter = characterItem;
                lastEndCharacter = characterItem;
                Debug.Log("Row: " + characterItem.Row + "  Col: " + characterItem.Col + "  text: " + characterItem.characterText.text + " IsHighlighted: " + characterItem.IsHighlighted);

                //active và set color cho Highlight khi người chơi chọn 1 từ
                AssignHighlighColor(selectingHighlight);
                selectingHighlight.gameObject.SetActive(true);

                UpdateSelectingHighlight(eventData.position);
                UpdateSelectedWord();
            }

            // Debug.Log(characterItem.Col);
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag: ");
        if (eventData.pointerId != selectingPointerId)
        {
            return;
        }

        if (GameManager.Instance.ActiveGameState == GameManager.GameState.BoardActive)
        {
            UpdateSelectingHighlight(eventData.position);
            UpdateSelectedWord();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp: ");
        if (eventData.pointerId != selectingPointerId)
        {
            return;
        }

        if (startCharacter != null && lastEndCharacter != null && GameManager.Instance.ActiveGameState == GameManager.GameState.BoardActive)
        {
            // trả lại màu đen cho chữ trên màn chơi
            SetTextColor(startCharacter, lastEndCharacter, letterColor, false);

            // VỊ trí bắt đầu và kết thúc
            Cell wordStartPosition = new Cell(startCharacter.Row, startCharacter.Col);
            Cell wordEndPosition = new Cell(lastEndCharacter.Row, lastEndCharacter.Col);

            //get text từ vị trí bắt đầu tới vị trí kết thúc
            string highlightedWord = GetWord(wordStartPosition, wordEndPosition);

            // Gọi GameManager kiểm tra xem word có đúng với yêu cầu k, nếu đúng sẽ trả lại word 
            string foundWord = GameManager.Instance.OnWordSelected(highlightedWord);

            // kiểm tra foundWord không phải null hoặc chuỗi rỗng
            if (!string.IsNullOrEmpty(foundWord))
            {
                ShowWord(wordStartPosition, wordEndPosition, foundWord, true);

                // SoundManager.Instance.Play("word-found");
            }
        }

        // End selecting and hide the select highlight
        isSelecting = false;
        selectingPointerId = -1;
        startCharacter = null;
        lastEndCharacter = null;
        selectingHighlight.gameObject.SetActive(false);
        selectedWord.Clear();
    }


    // Khởi tạo component
    public void Initialize()
    {
        //Chứa tất cả các chữ trên màn chơi
        gridContainer = CreateContainer("grid_container", typeof(RectTransform), typeof(GridLayoutGroup), typeof(CanvasGroup));

        // Chứa tất cả các phần hiện nên phía trên màn chơi. (chữ bay lên khi hoàn thành đúng)
        gridOverlayContainer = CreateContainer("grid_overlay_container", typeof(RectTransform));

        if (highlightPosition == HighlighPosition.BelowLetters)
        {
            //Chứa các phần màu highlight cho các từ đã được chọn đúng
            gridUnderlayContainer = CreateContainer("grid_underlay_container", typeof(RectTransform));

            gridUnderlayContainer.SetAsFirstSibling();
        }
        // Gợi ý các chữ cái mà người chơi cần tìm kiếm (Khoanh tròn các các chữ cái có trong màn chơi)
        highlighLetterContainer = CreateContainer("highligh_letter_container", typeof(RectTransform));

        characterItems = new List<List<CharacterGridItem>>();
        highlights = new List<Image>();

        // khởi tạo highlight cơ bản để dử dụng khi người chơi chọn 1 từ
        selectingHighlight = CreateNewHighlight();
        selectingHighlight.gameObject.SetActive(false);
    }

    // set chữ lên màn chơi
    public void SetUp(Board board)
    {
        currentCellSize = SetupGridContainer(board.rows, board.cols);
        currentScale = currentCellSize / maxCellSize;

        for (int i = 0; i < board.boardCharacters.Count; i++)
        {
            characterItems.Add(new List<CharacterGridItem>());
            for (int j = 0; j < board.boardCharacters[i].Count; j++)
            {
                char text = board.boardCharacters[i][j];
                GameObject _characterItem = Instantiate(characterGridItemPrefab, Vector3.zero, Quaternion.identity, gridContainer);
                CharacterGridItem _characterItemScript = _characterItem.GetComponent<CharacterGridItem>();
                _characterItemScript.Setup(text, new Vector3(currentScale, currentScale, 1f), ScaledLetterOffsetInCell);

                _characterItemScript.Row = i;
                _characterItemScript.Col = j;
                _characterItemScript.IsHighlighted = false;

                characterItems[i].Add(_characterItemScript);
            }
        }
        selectingPointerId = -1;

        currentBoard = board;
        // selectingHighlight = CreateNewHighlight();
        // selectingHighlight.gameObject.SetActive(false);
    }


    //set clolor Highligh
    private void AssignHighlighColor(Image highlight)
    {
        Color color = Color.white;

        if (highlightColors.Count > 0)
        {
            color = highlightColors[Random.Range(0, highlightColors.Count)];
        }
        else
        {
            Debug.LogError("[CharacterGrid] Highlight Colors is empty.");
        }

        highlight.color = color;
    }

    private void UpdateSelectingHighlight(Vector2 screenPosition)
    {
        if (isSelecting)
        {
            CharacterGridItem endCharacter = GetCharacterItemAtPosition(screenPosition);

            // If endCharacter is null then the mouse position must be off the grid container
            if (endCharacter != null)
            {
                int startRow = startCharacter.Row;
                int startCol = startCharacter.Col;

                int endRow = endCharacter.Row;
                int endCol = endCharacter.Col;

                int rowDiff = endRow - startRow;
                int colDiff = endCol - startCol;

                // check đường đi Highlight là thẳng, ngang hay chéo
                if (rowDiff != colDiff && rowDiff != 0 && colDiff != 0)
                {
                    // Lấy ra một vị trí kết thúc cuối cùng hợp lý nhất
                    if (Mathf.Abs(colDiff) > Mathf.Abs(rowDiff))
                    {
                        if (Mathf.Abs(colDiff) - Mathf.Abs(rowDiff) > Mathf.Abs(rowDiff))
                        {
                            rowDiff = 0;
                        }
                        else
                        {
                            colDiff = AssignKeepSign(colDiff, rowDiff);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(rowDiff) - Mathf.Abs(colDiff) > Mathf.Abs(colDiff))
                        {
                            colDiff = 0;
                        }
                        else
                        {
                            colDiff = AssignKeepSign(colDiff, rowDiff);
                        }
                    }

                    if (startCol + colDiff < 0)
                    {
                        colDiff = colDiff - (startCol + colDiff);
                        rowDiff = AssignKeepSign(rowDiff, Mathf.Abs(colDiff));
                    }
                    else if (startCol + colDiff >= currentBoard.cols)
                    {
                        colDiff = colDiff - (startCol + colDiff - currentBoard.cols + 1);
                        rowDiff = AssignKeepSign(rowDiff, Mathf.Abs(colDiff));
                    }

                    endCharacter = characterItems[startRow + rowDiff][startCol + colDiff];
                }
            }
            else
            {
                // Use the last selected end character
                endCharacter = lastEndCharacter;
            }

            if (lastEndCharacter != null)
            {
                // tìm và set màu chữ từ vị trí bắt đầu đến vị trí kết thúc
                SetTextColor(startCharacter, lastEndCharacter, letterColor, false);
            }

            // Set kích thước, vị trí , dóc nghiêng của Highlight
            PositionHighlight(selectingHighlight, startCharacter, endCharacter);

            // tìm và set màu chữ từ vị trí bắt đầu đến vị trí kết thúc
            SetTextColor(startCharacter, endCharacter, letterHighlightedColor, false);

            // If the new end character is different then the last play a sound
            if (lastEndCharacter != endCharacter)
            {
                // SoundManager.Instance.Play("highlight");
            }

            // Set the last end character so if the player drags outside the grid container then we have somewhere to drag to
            lastEndCharacter = endCharacter;
        }
    }

    // tìm và set màu chữ từ vị trí bắt đầu đến vị trí kết thúc
    private void SetTextColor(CharacterGridItem start, CharacterGridItem end, Color color, bool isHighlighted)
    {
        int rowInc = (start.Row == end.Row) ? 0 : (start.Row < end.Row ? 1 : -1);
        int colInc = (start.Col == end.Col) ? 0 : (start.Col < end.Col ? 1 : -1);
        int incAmount = Mathf.Max(Mathf.Abs(start.Row - end.Row), Mathf.Abs(start.Col - end.Col));

        for (int i = 0; i <= incAmount; i++)
        {
            CharacterGridItem characterGridItem = characterItems[start.Row + i * rowInc][start.Col + i * colInc];
            if (characterGridItem.IsHighlighted)
            {
                characterGridItem.characterText.color = letterHighlightedColor;
            }
            else
            {
                if (isHighlighted)
                {
                    characterGridItem.IsHighlighted = isHighlighted;
                }
                characterGridItem.characterText.color = color;
            }
        }
    }
    private void PositionHighlight(Image highlight, CharacterGridItem start, CharacterGridItem end)
    {
        RectTransform highlightRectT = highlight.transform as RectTransform;
        Vector2 startPosition = (start.transform as RectTransform).anchoredPosition;
        Vector2 endPosition = (end.transform as RectTransform).anchoredPosition;

        float distance = Vector2.Distance(startPosition, endPosition);
        float highlightWidth = currentCellSize + distance + ScaledHighlighExtraSize;
        float highlightHeight = currentCellSize + ScaledHighlighExtraSize;
        float scale = highlightHeight / highlight.sprite.rect.height;
        // Debug.Log("currentCellSize: " + currentCellSize + "   ScaledHighlighExtraSize: " + ScaledHighlighExtraSize + "  currentScale: " + currentScale);
        // Debug.Log("highlightHeight: " + highlightHeight + "   highlight.sprite.rect.height: " + highlight.sprite.rect.height + "  scale: " + scale);

        // Set position and size
        highlightRectT.anchoredPosition = startPosition + (endPosition - startPosition) / 2f;

        // Now Set the size of the highlight
        highlightRectT.localScale = new Vector3(scale, scale);
        highlightRectT.sizeDelta = new Vector2(highlightWidth / scale, highlight.sprite.rect.height);

        // Set angle
        float angle = Vector2.Angle(new Vector2(1f, 0f), endPosition - startPosition);

        if (startPosition.y > endPosition.y)
        {
            angle = -angle;
        }

        highlightRectT.eulerAngles = new Vector3(0f, 0f, angle);
    }


    //Set text header (SelectedWord)
    private void UpdateSelectedWord()
    {
        if (startCharacter != null && lastEndCharacter != null)
        {
            Cell wordStartPosition = new Cell(startCharacter.Row, startCharacter.Col);
            Cell wordEndPosition = new Cell(lastEndCharacter.Row, lastEndCharacter.Col);

            selectedWord.SetSelectedWord(GetWord(wordStartPosition, wordEndPosition), selectingHighlight.color);
        }
        else
        {
            selectedWord.Clear();
        }
    }

    //get text từ vị trí bắt đầu tới vị trí kết thúc
    private string GetWord(Cell start, Cell end)
    {
        int rowInc = (start.row == end.row) ? 0 : (start.row < end.row ? 1 : -1);
        int colInc = (start.col == end.col) ? 0 : (start.col < end.col ? 1 : -1);
        int incAmount = Mathf.Max(Mathf.Abs(start.row - end.row), Mathf.Abs(start.col - end.col));

        string word = "";

        for (int i = 0; i <= incAmount; i++)
        {
            word = word + currentBoard.boardCharacters[start.row + i * rowInc][start.col + i * colInc];
        }
        return word;
    }



    //Hàm bổ trợ


    // Get chữ từ vị trí
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
    private int AssignKeepSign(int a, int b)
    {
        return (a / Mathf.Abs(a)) * Mathf.Abs(b);
    }

    private Image CreateNewHighlight()
    {
        GameObject highlightObject = new GameObject("highlight");
        RectTransform highlightRectT = highlightObject.AddComponent<RectTransform>();
        Image highlightImage = highlightObject.AddComponent<Image>();

        highlightRectT.anchorMin = new Vector2(0f, 1f);
        highlightRectT.anchorMax = new Vector2(0f, 1f);
        highlightRectT.SetParent(highlightPosition == HighlighPosition.AboveLetters ? gridOverlayContainer : gridUnderlayContainer, false);

        highlightImage.type = Image.Type.Sliced;
        highlightImage.fillCenter = true;
        highlightImage.sprite = highlightSprite;

        AssignHighlighColor(highlightImage);

        if (selectingHighlight != null)
        {
            selectingHighlight.transform.SetAsLastSibling();
        }

        return highlightImage;
    }
    private RectTransform CreateContainer(string name, params System.Type[] types)
    {
        GameObject containerObj = new GameObject(name, types);
        RectTransform container = containerObj.GetComponent<RectTransform>();

        container.SetParent(transform, false);
        container.anchoredPosition = Vector2.zero;
        container.anchorMin = Vector2.zero;
        container.anchorMax = Vector2.one;
        container.offsetMin = Vector2.zero;
        container.offsetMax = Vector2.zero;

        return container;
    }

    private void ShowWord(Cell wordStartPosition, Cell wordEndPosition, string word, bool useSelectedColor)
    {
        CharacterGridItem startCharacter = characterItems[wordStartPosition.row][wordStartPosition.col];
        CharacterGridItem endCharacter = characterItems[wordEndPosition.row][wordEndPosition.col];

        Image highlight = HighlightWord(wordStartPosition, wordEndPosition, useSelectedColor);

        // Create the floating text in the middle of the highlighted word
        // Vector2 startPosition = (startCharacter.transform as RectTransform).anchoredPosition;
        // Vector2 endPosition = (endCharacter.transform as RectTransform).anchoredPosition;
        // Vector2 center = endPosition + (startPosition - endPosition) / 2f;

        // Text floatingText = CreateFloatingText(word, highlight.color, center);

        // Color toColor = new Color(floatingText.color.r, floatingText.color.g, floatingText.color.b, 0f);

    }
    public Image HighlightWord(Cell start, Cell end, bool useSelectedColour)
    {
        Image highlight = CreateNewHighlight();

        highlights.Add(highlight);

        CharacterGridItem startCharacterItem = characterItems[start.row][start.col];
        CharacterGridItem endCharacterItem = characterItems[end.row][end.col];

        // Set kích thước, vị trí , dóc nghiêng của Highlight
        PositionHighlight(highlight, startCharacterItem, endCharacterItem);

        // tìm và set màu chữ từ vị trí bắt đầu đến vị trí kết thúc
        SetTextColor(startCharacterItem, endCharacterItem, letterHighlightedColor, true);

        if (useSelectedColour && selectingHighlight != null)
        {
            highlight.color = selectingHighlight.color;
        }

        return highlight;
    }

    // private Text CreateFloatingText(string text, Color color, Vector2 position)
    // {
    //     GameObject floatingTextObject = new GameObject("found_word_floating_text", typeof(Shadow));
    //     RectTransform floatingTextRectT = floatingTextObject.AddComponent<RectTransform>();
    //     Text floatingText = floatingTextObject.AddComponent<Text>();

    //     floatingText.text = text;
    //     floatingText.font = letterFont;
    //     floatingText.fontSize = letterFontSize;
    //     floatingText.color = color;

    //     floatingTextRectT.anchoredPosition = position;
    //     floatingTextRectT.localScale = new Vector3(currentScale, currentScale, 1f);
    //     floatingTextRectT.anchorMin = new Vector2(0f, 1f);
    //     floatingTextRectT.anchorMax = new Vector2(0f, 1f);
    //     floatingTextRectT.SetParent(gridOverlayContainer, false);

    //     ContentSizeFitter csf = floatingTextObject.AddComponent<ContentSizeFitter>();
    //     csf.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    //     csf.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

    //     return floatingText;

    // }
    private float SetupGridContainer(int rows, int columns)
    {
        GridLayoutGroup gridLayoutGroup = gridContainer.GetComponent<GridLayoutGroup>();

        float cellWidth = gridContainer.rect.width / (float)columns;
        float cellHeight = gridContainer.rect.height / (float)rows;
        float cellSize = Mathf.Min(cellWidth, cellHeight, maxCellSize);

        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
        return cellSize;
    }



    public void ShowWordHint(string word)
    {
        if (currentBoard == null)
        {
            return;
        }

        for (int i = 0; i < currentBoard.wordPlacements.Count; i++)
        {
            Board.WordPlacement wordPlacement = currentBoard.wordPlacements[i];

            if (word == wordPlacement.word)
            {
                Cell startPosition = wordPlacement.startingPosition;
                Cell endPosition = new Cell(startPosition.row + wordPlacement.verticalDirection * (word.Length - 1), startPosition.col + wordPlacement.horizontalDirection * (word.Length - 1));

                ShowWord(startPosition, endPosition, word, false);

                break;
            }
        }
    }





}
