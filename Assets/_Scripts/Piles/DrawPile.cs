using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawPile : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _drawedPile;   
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        if(transform.childCount > 1)
        {
            Transform cardTransform = transform.GetChild(transform.childCount - 1);
            PlayPileDrop.AddCardOnPile(cardTransform.gameObject, _drawedPile);
            cardTransform.GetComponent<CardDisplay>().SetReverseVisibility(false);
        }
        else
        {
            ResetDrawPile();
        }
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
