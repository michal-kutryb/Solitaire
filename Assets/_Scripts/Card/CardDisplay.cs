using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    private CardInfo _cardInfo;

    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _suitText;

    private void Awake()
    {
        _cardInfo = gameObject.GetComponent<CardInfo>();
    }

    public void UpdateDisplay() 
    {
        _valueText.SetText(_cardInfo.Card.Value.ToString());
        _suitText.SetText(_cardInfo.Card.Suit.ToString());
    }
}
