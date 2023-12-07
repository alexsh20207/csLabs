using CardPickStrategy;

namespace sharpLab1
{
    class Program
    {
        static void Main()
        {
            var successExperiments = 0;
            var totalExperiments = Consts.EXPERIMENT_COUNT;
            for (var i = 0; i < totalExperiments; i++)
            {
                var deck = new Deck(Consts.DECK_CARD_COUNT, Consts.PLAYER_COUNT);
                var cardsPlayer1 = deck.GetCards(Consts.PLAYER1_NUM);
                var cardsPlayer2 = deck.GetCards(Consts.PLAYER2_NUM);

                var strategy1 = new FirstCardStrategy();
                var strategy2 = new FirstCardStrategy();

                var numPlayer1Card = strategy1.Pick(cardsPlayer1);
                var numPlayer2Card = strategy2.Pick(cardsPlayer2);

                var pickedCard1 = cardsPlayer1[numPlayer1Card];
                var pickedCard2 = cardsPlayer2[numPlayer2Card];

                if (pickedCard1.Color == pickedCard2.Color)
                {
                    successExperiments++;
                }
            }

            var probability = successExperiments / (double)totalExperiments;
            Console.WriteLine("Vicrories: " + successExperiments + "\n" +
                "Total experiments: " + totalExperiments + "\n" +
                "Probability: " + probability);
        }
    }
}