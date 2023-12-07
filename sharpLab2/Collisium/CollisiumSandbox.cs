using CardPickStrategy;

namespace sharpLab2;

public class CollisiumSandbox
{
    private IPartner _mark;
    public IPartner Mark { get => _mark; set => _mark = value; }
    private IPartner _elon;
    public IPartner Elon { get => _elon; set => _elon = value; }
    private IDeckShuffler _shufller { get; set; }
    private const int _markNum = 0;
    private const int _elonNum = 1;
    private Deck _deck;
    public Deck Deck { get => _deck; set => _deck = value; }
    private Deck[] _decks;
    public Deck[] Decks{ get => _decks; set => _decks = value; }

    public CollisiumSandbox(
        IEnumerable<IPartner> partners,
        IEnumerable<ICardPickStrategy> strategys, 
        IDeckShuffler shufller)
    {
        var partnersArray = partners.ToArray();
        _mark = partnersArray[_markNum];
        _elon = partnersArray[_elonNum];

        var strategysArray = strategys.ToArray();
        _mark.Strategy = strategysArray[_markNum];
        _elon.Strategy = strategysArray[_elonNum];

        _shufller = shufller;
    }

    public bool RunExperiment()
    {
        var players = new IPartner[] { _mark, _elon };

        _deck = new Deck(Consts.CARD_COUNT);
        _decks = _shufller.ShuffleDeck(_deck, players.Length);

        var pickNumCardofMark = _mark.Strategy.Pick(_decks[_markNum].Cards);
        var pickNumCardofElon = _elon.Strategy.Pick(_decks[_elonNum].Cards);

        var cardMark = GetCard(_markNum, pickNumCardofElon);
        var cardElon = GetCard(_elonNum, pickNumCardofMark);

        return cardElon.Color == cardMark.Color;
    }

    public Card GetCard(int playerNum,  int cardNum)
    {
        return _decks[playerNum].GetCard(cardNum);
    }
}