using CardPickStrategy;

namespace sharpLab2;

public class Deck
{
    private readonly int _countCards;

    public Card[] Cards { get; set; }

    public Card GetCard(int numCard)
    {
        return Cards[numCard];
    }

    public Deck(Card[] cards)
    {
        Cards = cards;
    }

    public Deck(int countCards)
    {
        _countCards = countCards;
        Cards = new Card[_countCards];
        for (int i = 0; i < _countCards; i += Consts.PLAYER_COUNT)
        {
            Cards[i] = new Card(Color.Red);
            Cards[i + 1] = new Card(Color.Black);
        }
    }
}