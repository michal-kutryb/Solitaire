using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawPile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _drawedPile;
    [SerializeField] private GameObject _draggedCardsBox;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(_draggedCardsBox.transform.childCount != 0) 
        {
            return;
        }
        if(transform.childCount > 1)
        {
            Transform cardTransform = transform.GetChild(transform.childCount - 1);
            PlayPileDrop.AddCardOnPile(cardTransform.gameObject, _drawedPile);
            cardTransform.GetComponent<CardDisplay>().SetReverseVisibility(false);
            GameManager.Instance.isDrawing = true;
        }
        else
        {
            ResetDrawPile();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.isDrawing = false;
    }

    private void ResetDrawPile()
    {
        while(_drawedPile.transform.childCount > 0)
        {
            Transform childCard = _drawedPile.transform.GetChild(_drawedPile.transform.childCount - 1);
            PlayPileDrop.AddCardOnPile(childCard.gameObject, gameObject);
            childCard.gameObject.GetComponent<CardDisplay>().SetReverseVisibility(true);
        }
    }
}
