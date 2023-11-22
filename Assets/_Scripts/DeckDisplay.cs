using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    private Deck _deck;

    private void Start()
    {
        _deck = new Deck();
    }

    public void ShowCards() //DEBUG to temporary show some cards in deck
    {
        for(int i = 0; i < _deck.Cards.Count; i++) 
        {
            Debug.Log(_deck.Cards[i].Value.ToString() + " of " + _deck.Cards[i].Suit.ToString());
        }
    }

    public void ShuffleDeck() //DEBUG to test deck.Shuffle method
    {
        _deck.Shuffle();

        Debug.Log("===================");
        Debug.Log("Shuffling Completed");
        Debug.Log("===================");
    }
}
