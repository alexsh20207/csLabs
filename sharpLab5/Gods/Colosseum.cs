using System.Text;
using sharpLab2;
using CardPickStrategy;

namespace Gods;

public class Colosseum
{
    private const int DECK_CARD_COUNT = 36;
    private const string elonURL = "http://localhost:1234/ElonRoom";
    private const string markURL = "http://localhost:1235/MarkRoom";

    public CollisiumSandbox CreateCollisiumSandbox()
    {
        IDeckShuffler deckShuffler = new DeckShuffler();
        var partners = new List<IPartner> { new Mark(), new Elon() };
        var strategies = new List<ICardPickStrategy> { new FirstCardStrategy(), new FirstCardStrategy() };

        return new CollisiumSandbox(partners, strategies, deckShuffler);
    }

    public async Task Send()
    {
        CollisiumSandbox sandbox = CreateCollisiumSandbox();
        sandbox.RunExperiment();

        Card[] cardsMark = new Card[DECK_CARD_COUNT / 2];
        Card[] cardsElon = new Card[DECK_CARD_COUNT / 2];

        for (int i = 0; i < DECK_CARD_COUNT / 2; i++)
        {
            cardsMark[i] = sandbox.GetCard(0, i);
            cardsElon[i] = sandbox.GetCard(1, i);
        }

        string elonJson = Newtonsoft.Json.JsonConvert.SerializeObject(cardsElon);
        string markJson = Newtonsoft.Json.JsonConvert.SerializeObject(cardsMark);

        using (HttpClient client = new HttpClient())
        {
            StringContent elonContent = new StringContent(elonJson, Encoding.UTF8, "application/json");
            StringContent markContent = new StringContent(markJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseElonRoom = await client.PostAsync(elonURL, elonContent);
            HttpResponseMessage responseMarkRoom = await client.PostAsync(markURL, markContent);

            if (responseElonRoom.IsSuccessStatusCode && responseMarkRoom.IsSuccessStatusCode)
            {
                string elonResult = await responseElonRoom.Content.ReadAsStringAsync();
                string markResult = await responseMarkRoom.Content.ReadAsStringAsync();

                int elonPick;
                int markPick;

                int.TryParse(elonResult, out elonPick);
                int.TryParse(markResult, out markPick);

                Card cardElon = cardsElon[markPick];
                Card cardMark = cardsMark[elonPick];

                bool result = (cardElon.Color == cardMark.Color);

                Console.WriteLine($"Pick Cards: Elon {elonPick} - " + $"Mark: {markPick}\n" +
                    $"Card Color: Elon {cardElon.Color} - " + $"Mark: {cardMark.Color}\n" +
                    $"Result: {result}");
            }
            else
            {
                Console.WriteLine("Error:\n" + 
                    $"Elon Status: {responseElonRoom.StatusCode}\n" +
                    $"Mark Status: {responseMarkRoom.StatusCode}");
            }
        }
    }

}