public class Card
{
    public CardValue Value { get; private set; }
    public CardSuit Suit { get; private set; }

    public Card(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }
}
