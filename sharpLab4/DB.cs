using System.Text.Json;
using System.Xml.Linq;
using sharpLab2;
using CardPickStrategy;

namespace sharpLab4;

public class DB
{
    private List<Experiment> _experiments;
    public List<Experiment> experiments { get => _experiments; set => _experiments = value; }

    private ApplicationContext _db;

    public void createContext(DbName dbName)
    {
        ApplicationContext.SetDb(dbName);
        _db = new ApplicationContext();
    }

    private CollisiumSandbox CreateCollisiumSandbox()
    {
        IDeckShuffler deckShuffler = new DeckShuffler();
        var partners = new List<IPartner> { new Mark(), new Elon() };
        var strategies = new List<ICardPickStrategy> { new FirstCardStrategy(), new FirstCardStrategy() };

        return new CollisiumSandbox(partners, strategies, deckShuffler);
    }

    public void CreateExperiments(int number)
    {
        _experiments = new List<Experiment>();
        for (int i = 0; i < number; i++)
        {
            CollisiumSandbox collisiumSandbox = CreateCollisiumSandbox();
            bool result = collisiumSandbox.RunExperiment();

            Card[] cardsMark = new Card[Consts.DECK_CARD_COUNT / 2];
            Card[] cardsElon = new Card[Consts.DECK_CARD_COUNT / 2];

            for (int j = 0; j < Consts.DECK_CARD_COUNT / 2; j++)
            {
                cardsMark[j] = collisiumSandbox.GetCard(0, j);
                cardsElon[j] = collisiumSandbox.GetCard(1, j);
            }

            string cardsMarkJson = JsonSerializer.Serialize(cardsMark);
            string cardsElonJson = JsonSerializer.Serialize(cardsElon);

            Experiment experiment = new Experiment
            {
                pickNumCardofMark = collisiumSandbox.Mark.Strategy.Pick(cardsMark),
                pickNumCardofElon = collisiumSandbox.Elon.Strategy.Pick(cardsElon),
                cardsMark = cardsMarkJson,
                cardsElon = cardsElonJson,
                result = result
            };

            _experiments.Add(experiment);
        }
    }

    public void Save()
    {
        foreach (Experiment ex in _experiments)
        {
            _db.Experiments.AddRange(ex);
            _db.SaveChanges();
        }
    }

    public void Read()
    {
        _experiments = _db.Experiments.ToList();
    }
    
    public List<CollisiumSandbox> ExperimentsToSandbox()
    {
        List<CollisiumSandbox> sandboxs = new List<CollisiumSandbox>();
        if (_experiments != null)
        {
            foreach (Experiment ex in _experiments)
            {
                CollisiumSandbox collisiumSandbox = CreateCollisiumSandbox();
                
                Card[] cardsElon = JsonSerializer.Deserialize<Card[]>(ex.cardsElon);
                Card[] cardsMark = JsonSerializer.Deserialize<Card[]>(ex.cardsMark);

                collisiumSandbox.Decks = new Deck[] { new Deck(cardsMark), new Deck(cardsElon) };

                collisiumSandbox.Mark.Strategy = new CardStrategy(ex.pickNumCardofMark);
                collisiumSandbox.Elon.Strategy = new CardStrategy(ex.pickNumCardofElon);

                sandboxs.Add(collisiumSandbox);
            }
        }
        return sandboxs;
    }


    public int CountSuccesOfExperements(List<CollisiumSandbox> sandboxs)
    {
        return sandboxs.Count(sandboxs => sandboxs.RunExperiment() == true);
    }
}