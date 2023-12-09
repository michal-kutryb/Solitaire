using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas canvas;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();   
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _canvasGroup.blocksRaycasts = false;
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            GameObject topCard = GetTopCardOnPile(gameObject);
            AddCardOnCard(eventData.pointerDrag.gameObject, topCard);


        }
    }

    private GameObject GetTopCardOnPile(GameObject card)
    {
        Transform resultCard = card.transform;

        while(resultCard.Find("Card(Clone)") != null)
        {
            resultCard = resultCard.Find("Card(Clone)");
        }

        return resultCard.gameObject;
    }

    public void AddCardOnCard(GameObject droppedCard, GameObject card)
    {
        droppedCard.transform.SetParent(card.transform);
        RectTransform droppedCardPosition = droppedCard.GetComponent<RectTransform>();
        droppedCardPosition.localPosition = new Vector2(0, -75);
    }
}
