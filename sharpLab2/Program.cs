using CardPickStrategy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace sharpLab2;
class Program
{
    public static void Main()
    {
        CreateHostBuilder().Build().Run();
    }
    public static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<CollisiumExperimentWorker>();
                services.AddScoped<CollisiumSandbox>();
                services.AddScoped<Deck>();
                services.AddScoped<IDeckShuffler, DeckShuffler>();
                services.AddScoped<ICardPickStrategy, FirstCardStrategy>();
                services.AddScoped<ICardPickStrategy, FirstCardStrategy>();
                services.AddScoped<IPartner, Mark>();
                services.AddScoped<IPartner, Elon>();
            });
    }
}