using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    [HideInInspector] public bool isDropSucces;

    public Transform ParentBeforeDrop { get; private set; }

    private void Awake()
    {
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
    }
}
