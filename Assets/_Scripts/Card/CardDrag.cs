using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;

    private GameManager _gameManager;//tu change by�
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [HideInInspector] public bool isDropSucces;

    public Transform ParentBeforeDrop { get; private set; }

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _rectTransform = GetComponent<RectTransform>();   
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponent<CardDisplay>().CardReverse.activeSelf)
        {
            eventData.pointerDrag = null;
            return;
        }

        Debug.Log("OnBeginDrag");
        isDropSucces = false;
        _canvasGroup.blocksRaycasts = false;

        ParentBeforeDrop = transform.parent; 
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        _gameManager.isDraggingACard = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
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
