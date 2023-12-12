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
            if (eventData.pointerDrag.GetComponent<CardInfo>().Card.Value.ToString() != "King")
                return;
            CardDragDrop droppedCardDragDrop = eventData.pointerDrag.GetComponent<CardDragDrop>();
            Transform reverseOfParentBeforeDrop = droppedCardDragDrop.ParentBeforeDrop.Find("Reverse");
            if (reverseOfParentBeforeDrop != null)
            {
                reverseOfParentBeforeDrop.gameObject.SetActive(false);
            }

            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector2.zero;
            eventData.pointerDrag.GetComponent<CardDragDrop>().isDropSucces = true;
        }
    }
}
