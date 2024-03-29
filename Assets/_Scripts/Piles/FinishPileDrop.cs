using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinishPileDrop : MonoBehaviour, IDropHandler
{
    [field:SerializeField] public CardSuit PileSuit { get; private set; }
    public int pileSize;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedCard = eventData.pointerDrag.gameObject;
        CardDrag droppedCardDragDrop = droppedCard.GetComponent<CardDrag>();
        CardInfo droppedCardInfo = droppedCard.GetComponent<CardInfo>();
        Transform reverseOfParentBeforeDrop = droppedCardDragDrop.ParentBeforeDrop.Find("Reverse");
        bool isParentReversed = false;
        if (reverseOfParentBeforeDrop != null && reverseOfParentBeforeDrop.gameObject.activeSelf)
        {
            isParentReversed = true;
        }

        if (!droppedCardInfo.Card.Suit.Equals(PileSuit) || !((int) droppedCardInfo.Card.Value == pileSize) || droppedCard.transform.childCount > 5)
        {
            return;
        }

        PlayPileDrop.AddCardOnPile(droppedCard, gameObject);
        MovesHistory.Instance.AddMove(droppedCard, droppedCardDragDrop.ParentBeforeDrop.gameObject, isParentReversed);

        if (reverseOfParentBeforeDrop != null)
        {
            reverseOfParentBeforeDrop.gameObject.SetActive(false);
        }

        droppedCardDragDrop.isDropSucces = true;
        pileSize++;
    }

    private void Update()
    {
        if(pileSize == 13)
        {
            GameManager.Instance.finishPilesCompleted[(int)PileSuit] = true;
        }
        else
        {
            GameManager.Instance.finishPilesCompleted[(int)PileSuit] = false;
        }
    }
}
