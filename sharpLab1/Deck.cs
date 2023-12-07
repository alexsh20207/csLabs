using CardPickStrategy;

namespace sharpLab1;
public class Deck
{
    private readonly Card[] _cards;
    private readonly int _countCards;
    private readonly int _countPlayers;

    public Deck(int countCards, int countPlayers)
    {
        _countCards = countCards;
        _countPlayers = countPlayers;

        _cards = new Card[_countCards];
        for (var i = 0; i < _countCards; i += _countPlayers)
        {
            _cards[i] = new Card(Color.Red);
            _cards[i + 1] = new Card(Color.Black);
        }
        ShuffleCards();
    }

    private void ShuffleCards()
    {
        Random rand = new();
        for (int i = 0; i < _countCards; i++)
        {
            var numChangeCard1 = rand.Next(_countCards);
            var numChangeCard2 = rand.Next(_countCards);

            while (numChangeCard1 == numChangeCard2)
            {
                numChangeCard2 = rand.Next(_countCards);
            }

            (_cards[numChangeCard2], _cards[numChangeCard1]) = (_cards[numChangeCard1], _cards[numChangeCard2]);
        }
    }

    public Card[] GetCards(int numPlayer)
    {
        var numberPlayerCards = _countCards / _countPlayers;
        var numStartCard = numberPlayerCards * (numPlayer - 1);

        return _cards.Skip(numStartCard)
            .Take(numberPlayerCards)
            .ToArray();
    }

}
