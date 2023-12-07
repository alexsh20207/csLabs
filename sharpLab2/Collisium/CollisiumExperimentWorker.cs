using Microsoft.Extensions.Hosting;
namespace sharpLab2;

public class CollisiumExperimentWorker : BackgroundService
{
    private readonly CollisiumSandbox _sandbox;

    public CollisiumExperimentWorker(CollisiumSandbox sandbox)
    {
        _sandbox = sandbox;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var totalExperiments = Consts.EXPERIMENT_COUNT;
        var successExperiments = 0;

        for (var i = 0; i < totalExperiments; i++)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                break;
            }

            var isSuccess = _sandbox.RunExperiment();
            if (isSuccess)
            {
                successExperiments++;
            }
        }

        var probability = successExperiments / (double)totalExperiments;
        Console.WriteLine("Vicrories: " + successExperiments + "\n" +
            "Total experiments: " + totalExperiments + "\n" +
            "Probability: " + probability);
        return Task.CompletedTask;
    }
}