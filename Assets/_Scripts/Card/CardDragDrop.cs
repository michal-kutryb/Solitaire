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
            if (ParentBeforeDrop.childCount != 0)
                AddCardOnCard(gameObject, ParentBeforeDrop.gameObject);
            else
                AddCardOnPile(gameObject, ParentBeforeDrop.gameObject);
        }
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
            CardInfo droppedCardInfo = eventData.pointerDrag.GetComponent<CardInfo>();
            GameObject topCard = GetTopCardOnPile(gameObject);
            CardInfo topCardInfo = topCard.GetComponent<CardInfo>();

            if (!CheckIfPossibleToDropOnCard(droppedCardInfo.Card, topCardInfo.Card))
                return;

            CardDragDrop droppedCardDragDrop = eventData.pointerDrag.GetComponent<CardDragDrop>();
            Transform reverseOfParentBeforeDrop = droppedCardDragDrop.ParentBeforeDrop.Find("Reverse");
            if(reverseOfParentBeforeDrop != null)
            {
                reverseOfParentBeforeDrop.gameObject.SetActive(false);
            }
            
            AddCardOnCard(eventData.pointerDrag.gameObject, topCard);
            droppedCardDragDrop.isDropSucces = true;
            
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

    public bool CheckIfPossibleToDropOnCard(Card droppedCard, Card card)
    {
        if (droppedCard.Value.ToString() == "King")
            return false;
        if (Card.CompareSuitsByColor(droppedCard.Suit, card.Suit))
            return false;
        if (!((int)droppedCard.Value + 1 == (int)card.Value))
            return false;

        return true;
    }

    public static void AddCardOnPile(GameObject droppedCard, GameObject pile)
    {
        droppedCard.transform.SetParent(pile.transform);
        droppedCard.GetComponent<RectTransform>().localPosition = Vector2.zero;
        droppedCard.GetComponent<CardDragDrop>().isDropSucces = true;
    }
}
