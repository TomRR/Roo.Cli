using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Roo.Cli.Cli.Nuget;

public class CliAppBuilder
{
    private readonly IHostBuilder _builder;

    private CliAppBuilder(IHostBuilder builder)
    {
        _builder = builder;
    }

    public static CliAppBuilder Create(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            });

        return new CliAppBuilder(builder);
    }

    public CliAppBuilder AddDependencies(Action<IServiceCollection> configureServices)
    {
        _builder.ConfigureServices(configureServices);
        return this;
    }

    public CliApp Build()
    {
        var host = _builder.Build();
        return new CliApp(host);
    }

    public async Task RunAsync(string[] args) =>
        await Build().RunAsync(args);
}