using CardPickStrategy;
using Moq;
using sharpLab2;
using Xunit;
using FluentAssertions;

namespace sharpLab3;

public class CollisiumSandboxTests
{
    private const int STRATEGY_PICK_NUM = 0;
    private const int DECK_CARD_COUNT = 36;
    private const int PLAYER_COUNT = 2;

    private Mock<IDeckShuffler>? _mockShuffler;
    private List<IPartner>? _partners;
    private List<ICardPickStrategy>? _strategies;

    [Fact]
    public void TestCorrectExperimentResult()
    {
        ArrangeVars();

        CollisiumSandbox sandbox = new CollisiumSandbox(_partners, _strategies, _mockShuffler.Object);
        var result = sandbox.RunExperiment();
        
        _mockShuffler.Verify(s => s.ShuffleDeck(It.IsAny<Deck>(), It.IsAny<int>()), Times.Once);
        result.Should().Be(false);
    }

    private void ArrangeVars()
    {
        var mockMark = new Mock<IPartner>();
        var mockElon = new Mock<IPartner>();
        var mockMarkCards = new Card[DECK_CARD_COUNT / PLAYER_COUNT];
        var mockElonCards = new Card[DECK_CARD_COUNT / PLAYER_COUNT] ;
        var mockFirstCardStrategy = new Mock<FirstCardStrategy>();
        _mockShuffler = new Mock<IDeckShuffler>();
        mockMark.Setup(m => m.Strategy.Pick(It.IsAny<Card[]>())).Returns(STRATEGY_PICK_NUM);
        mockElon.Setup(m => m.Strategy.Pick(It.IsAny<Card[]>())).Returns(STRATEGY_PICK_NUM);
        
        for (var i = 0; i < DECK_CARD_COUNT / PLAYER_COUNT; i++)
        {
            mockMarkCards[i] = new Card(Color.Red);
            mockElonCards[i] = new Card(Color.Black);
        }
        _partners = new List<IPartner> { mockMark.Object, mockElon.Object };
        _strategies = new List<ICardPickStrategy> {
            mockFirstCardStrategy.Object,
            mockFirstCardStrategy.Object
        };
        var decks = new Deck[] { new Deck(mockMarkCards), new Deck(mockElonCards) };
        _mockShuffler.Setup(s => s.ShuffleDeck(It.IsAny<Deck>(), It.IsAny<int>())).Returns(decks);
    }
}