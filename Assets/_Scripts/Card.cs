public class Card
{
    public readonly CardValue Value;
    public readonly CardSuit Suit;

    public Card(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }
}
