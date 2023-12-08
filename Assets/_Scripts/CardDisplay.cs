using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card Card { get; private set; }
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _suitText;

    public void SetCard(Card card)
    {
        Card = card;
        UpdateDisplay();
    }

    public void UpdateDisplay() 
    {
        //SET TEXT
        _valueText.SetText(Card.Value.ToString());
        _suitText.SetText(Card.Suit.ToString());
    }
}
