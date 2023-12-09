public class Card
{
    public readonly CardValue Value;
    public readonly CardSuit Suit;

    public Card(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }

    public static bool CompareSuitsByColor(CardSuit suit1, CardSuit suit2)
    {
        if (suit1 == CardSuit.Clubs || suit1 == CardSuit.Spades) 
        {
            if (suit2 == CardSuit.Clubs || suit2 == CardSuit.Spades)
                return true;
        }
        else 
        {
            if (suit2 != CardSuit.Clubs && suit2 != CardSuit.Spades)
                return true;
        }

        return false;
    }
}
