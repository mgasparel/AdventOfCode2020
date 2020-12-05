using AdventOfCode2020.Infrastructure;
using AdventOfCode2020.Runner;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
RegisterServices(serviceCollection);

using(var services = serviceCollection.BuildServiceProvider())
{
    services
        .GetRequiredService<PuzzleRunner>()
        .Run();
}

static void RegisterServices(IServiceCollection services)
{
    services.AddSingleton(typeof(PuzzleRunner));
    services.AddSingleton(typeof(PuzzleLocator));
    services.AddSingleton(typeof(PuzzleFactory));
}
