using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    public Card Card { get; private set; }

    private CardDisplay _cardDisplay;

    private void Awake()
    {
        _cardDisplay = gameObject.GetComponent<CardDisplay>();
    }

    public void SetCard(Card card)
    {
        Card = card;
        _cardDisplay.UpdateDisplay();
    }
}
