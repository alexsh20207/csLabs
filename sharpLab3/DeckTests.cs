using Xunit;
using sharpLab2;
using CardPickStrategy;
using FluentAssertions;

namespace sharpLab3;
public class DeckTests
{
    private int DECK_CARD_COUNT = 36;
    [Fact]
    public void TestSameCountOfCardWithDifferentColors()
    {
        var deck = new Deck(DECK_CARD_COUNT);

        var redCardCount = deck.Cards.Count(card => card.Color == Color.Red);
        var blackCardCount = deck.Cards.Count(card => card.Color == Color.Black); 

        redCardCount.Should().Be(DECK_CARD_COUNT / 2);
        blackCardCount.Should().Be(DECK_CARD_COUNT / 2);
    }
}