using CardPickStrategy;
using sharpLab2;

public class DeckShuffler : IDeckShuffler
{
    public Deck[] ShuffleDeck(Deck deck, int countPlayers)
    {
        var rand = new Random();
        var cards = deck.Cards;

        for (int i = 0; i < cards.Length; i++)
        {
            int numChangeCard1 = rand.Next(cards.Length);
            int numChangeCard2 = rand.Next(cards.Length);

            while (numChangeCard1 == numChangeCard2)
            {
                numChangeCard2 = rand.Next(cards.Length);
            }

            (cards[numChangeCard1], cards[numChangeCard2]) = (cards[numChangeCard2], cards[numChangeCard1]);
        }
        deck.Cards = cards;
        return GiveDeckforPlayer(deck, countPlayers);
    }

    public Deck[] GiveDeckforPlayer(Deck deck,int countPlayers)
    {
        int countPlayerCards = deck.Cards.Length / countPlayers;
        Deck[] decks = new Deck[countPlayers];

        for (int i = 0; i < countPlayers; i++)
        {
            Card[] cardsPlayer = new Card[countPlayerCards];
            for (int j = 0; j < countPlayerCards; j++)
            {
                cardsPlayer[j] = deck.Cards[j + i * countPlayerCards];
            }
            decks[i] = new Deck(cardsPlayer);
        }

        return decks;
    }
}