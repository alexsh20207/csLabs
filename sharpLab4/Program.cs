namespace sharpLab4;
class Program
{
    static void Main(string[] args)
    {
        DB db = new DB();
        int totalExperiments = 100;
        db.CreateExperiments(totalExperiments);

        int successExperiments = db.CountSuccesOfExperements(db.ExperimentsToSandbox());
        var probability = successExperiments / (double)totalExperiments;
        
        Console.WriteLine("Vicrories: " + successExperiments + "\n" +
            "Total experiments: " + totalExperiments + "\n" +
            "Probability: " + probability);
    }
}