using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _drawPile;

    private Deck _deck;

    private void Start()
    {
        _deck = new Deck();
        _deck.Shuffle();
        DisplayCards();
    }

    public void DisplayCards() 
    {
        int numCardsOnPile = 1;
        foreach (RectTransform child in transform)
        {
            RectTransform playPile = child;
            RectTransform beforeParent = InstantiateCard(child, true);

            for (int i=1; i<numCardsOnPile; i++) 
            {
                beforeParent.GetComponent<CardDisplay>().SetReverseVisibility(true);
                beforeParent = InstantiateCard(beforeParent, false);
            }
            numCardsOnPile++;
        }
        
        while(_deck.Cards.Count > 0)
        {
            RectTransform card = InstantiateCard(_drawPile.transform, true);
            card.GetComponent<CardDisplay>().SetReverseVisibility(true);
        }
    }

    public RectTransform InstantiateCard(Transform parent, bool isFirst)
    {
        GameObject card = Instantiate(_cardPrefab);

        card.GetComponent<CardInfo>().SetCard(_deck.Pop());
        card.transform.SetParent(parent, false);
        card.GetComponent<CardDragDrop>().canvas = _canvas;

        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        if (isFirst) 
        {
            cardRectTransform.localPosition = Vector2.zero;
        }
        else 
        {
            cardRectTransform.localPosition = new Vector2(0, -75);
        }

        return cardRectTransform;
    }
}
