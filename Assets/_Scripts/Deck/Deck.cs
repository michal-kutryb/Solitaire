using System.Collections.Generic;

public class Deck
{
    public List<Card> Cards { get; private set; }

    public Deck() 
    {
       Cards = new List<Card>();
       for(int i=0; i < 13; i++) 
       {
            Cards.Add(new Card((CardValue)i, CardSuit.Hearts));
            Cards.Add(new Card((CardValue)i, CardSuit.Diamonds));
            Cards.Add(new Card((CardValue)i, CardSuit.Clubs));
            Cards.Add(new Card((CardValue)i, CardSuit.Spades));
       }
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < Cards.Count; i++) 
        {
            Card temp = Cards[i];
            int randomIndex = random.Next(Cards.Count);
            Cards[i] = Cards[randomIndex];
            Cards[randomIndex] = temp;
        }
    }

    public Card Pop() 
    {
        if(Cards.Count == 0) 
        {
            return null;
        }
        Card chosen = Cards[Cards.Count - 1];
        Cards.RemoveAt(Cards.Count - 1);

        return chosen;
    }
}
