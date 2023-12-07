using CardPickStrategy;
using FluentAssertions;
using Xunit;

namespace sharpLab3;

public class StrategyTests
{
    private const int CARD_COUNT = 36;
    [Fact]
    public void TestStrategyOnShuffledDeck()
    {
        var strategy = new FirstCardStrategy();
        var cardRed = new Card[CARD_COUNT / 2];
        var cardBlack = new Card[CARD_COUNT / 2];
        for (var i = 0; i < CARD_COUNT / 2; i++)
        {
            cardRed[i] = new Card(Color.Red);
            cardBlack[i] = new Card(Color.Black);
        }

        var pickedCardRed = strategy.Pick(cardRed);
        var pickedCardBlack = strategy.Pick(cardBlack);

        cardRed[0].Color.Should().Be(Color.Red);
        pickedCardRed.Should().Be(0);
        cardBlack[0].Color.Should().Be(Color.Black);
        pickedCardBlack.Should().Be(0);
    }
}
