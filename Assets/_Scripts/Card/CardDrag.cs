using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;

    private GameManager _gameManager;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [HideInInspector] public bool isDropSucces;

    public Transform ParentBeforeDrop { get; private set; }
    private Transform _draggedCardsBox;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _draggedCardsBox = canvas.transform.GetChild(7);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponent<CardDisplay>().CardReverse.activeSelf || _draggedCardsBox.childCount != 0 || GameManager.Instance.isDrawing)
        {
            eventData.pointerDrag = null;
            return;
        }

        isDropSucces = false;
        _canvasGroup.blocksRaycasts = false;

        ParentBeforeDrop = transform.parent; 
        transform.SetParent(_draggedCardsBox);
        transform.SetAsLastSibling();
        _gameManager.isDraggingACard = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        if (!isDropSucces)
        {
            if (ParentBeforeDrop.name == "Card(Clone)")
                PlayPileDrop.AddCardOnCard(gameObject, ParentBeforeDrop.gameObject);
            else
                PlayPileDrop.AddCardOnPile(gameObject, ParentBeforeDrop.gameObject);
        }
        _gameManager.isDraggingACard = false;
    }
}
