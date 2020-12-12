using AdventOfCode2020.Infrastructure;
using AdventOfCode2020.Runner;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
RegisterServices(serviceCollection);

using (ServiceProvider? services = serviceCollection.BuildServiceProvider())
{
    services
        .GetRequiredService<PuzzleRunner>()
        .Run();
}

static void RegisterServices(IServiceCollection services)
{
    _ = services
        .AddSingleton(typeof(PuzzleRunner))
        .AddSingleton(typeof(PuzzleLocator))
        .AddSingleton(typeof(PuzzleFactory));
}
