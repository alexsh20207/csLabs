using FluentAssertions;
using sharpLab4;
using Xunit;

namespace sharpLab3;
public class DBTest
{
    private const int EXPERIMENT_COUNT = 100;
    [Fact]
    public void TestDBOnSaveRead()
    {
        DB db = new DB();
        db.createContext(DbName.LocalSQL);
        
        db.CreateExperiments(EXPERIMENT_COUNT);
        List<Experiment> experimentsSave = db.experiments;
        db.Save();
        db.Read();
        List<Experiment> experimentsRead = db.experiments;

        for (int i = 0; i < EXPERIMENT_COUNT; i++)
        {
            experimentsSave[i].result.Should().Be(experimentsRead[i].result);
        }
    }
}