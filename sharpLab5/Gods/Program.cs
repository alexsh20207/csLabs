namespace Gods;
class Program
{
    static void Main()
    {
        Colosseum colosseum = new Colosseum();
        string input = Console.ReadLine();
        while (input != "q")
        {
            colosseum.Send();
            input = Console.ReadLine();
        }
    }
}