using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayPileDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            GameObject droppedCard = eventData.pointerDrag.gameObject;
            CardDragDrop droppedCardDragDrop = droppedCard.GetComponent<CardDragDrop>();
            Transform reverseOfParentBeforeDrop = droppedCardDragDrop.ParentBeforeDrop.Find("Reverse");

            if (transform.childCount == 0)
            {
                if (!droppedCard.GetComponent<CardInfo>().Card.Value.Equals(CardValue.King))
                {
                    return;
                }

                AddCardOnPile(droppedCard, gameObject);

            }
            else
            {
                GameObject topCard = GetTopCardOnPile(gameObject);
                if (IsCardReversed(topCard))
                {
                    return;
                }

                CardInfo topCardInfo = topCard.GetComponent<CardInfo>();
                CardInfo droppedCardInfo = eventData.pointerDrag.GetComponent<CardInfo>();
                if (!CheckIfPossibleToDropOnCard(droppedCardInfo.Card, topCardInfo.Card))
                {
                    return;
                }
                AddCardOnCard(droppedCard, topCard);
            }

            if (reverseOfParentBeforeDrop != null)
            {
                reverseOfParentBeforeDrop.gameObject.SetActive(false);
            }

            droppedCardDragDrop.isDropSucces = true;

            if (droppedCardDragDrop.ParentBeforeDrop.name.Contains("Finish"))
            {
                droppedCardDragDrop.ParentBeforeDrop.GetComponent<FinishPileDrop>().pileSize--;
            }
        }
    }


    private GameObject GetTopCardOnPile(GameObject card)
    {
        Transform resultCard = card.transform;

        while (resultCard.Find("Card(Clone)") != null)
        {
            resultCard = resultCard.Find("Card(Clone)");
        }

        return resultCard.gameObject;
    }
    public static void AddCardOnCard(GameObject droppedCard, GameObject card)
    {
        droppedCard.transform.SetParent(card.transform);
        RectTransform droppedCardPosition = droppedCard.GetComponent<RectTransform>();
        droppedCardPosition.localPosition = new Vector2(0, -75);
    }

    public static bool CheckIfPossibleToDropOnCard(Card droppedCard, Card card)
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
    }

    private bool IsCardReversed(GameObject Card)
    {
        Transform reverse = Card.transform.Find("Reverse");
        return reverse.gameObject.activeSelf;
    }
}
