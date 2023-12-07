using CardPickStrategy;
namespace sharpLab2;

public interface IDeckShuffler
{
    public Deck[] ShuffleDeck(Deck deck, int countPlayers);
}