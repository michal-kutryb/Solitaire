using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    private CardInfo _cardInfo;
    public GameObject CardReverse { get; private set; }
    [SerializeField] private Sprite[] _suitSprites;

    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Image _suitBigImage;
    [SerializeField] private Image _suitSmallImage;

    private void Awake()
    {
        _cardInfo = gameObject.GetComponent<CardInfo>();
        CardReverse = transform.Find("Reverse").gameObject;
    }

    public void UpdateDisplay()
    {
        _valueText.SetText(GetValueSign(_cardInfo.Card.Value).ToString());
        Sprite suitSprite = _suitSprites[(int)_cardInfo.Card.Suit];
        _suitBigImage.sprite = suitSprite;
        _suitSmallImage.sprite = suitSprite;

        if (_cardInfo.Card.Suit.Equals(CardSuit.Hearts) || _cardInfo.Card.Suit.Equals(CardSuit.Diamonds))
        {
            _valueText.color = Color.red;
        }
    }

    public void SetReverseVisibility(bool value)
    {
        CardReverse.SetActive(value);
    }

    private string GetValueSign(CardValue val)
    {
        int index = (int)val;
        if (index == 0 || index >= 10) 
        {
            return val.ToString()[0].ToString();
        }
        return (index + 1).ToString();
    }
}
