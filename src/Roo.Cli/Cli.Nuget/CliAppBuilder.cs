using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Roo.Cli.Cli.Nuget;
public class CliAppBuilder
{
    private readonly IHostBuilder _builder;
    private readonly List<Action<IHost>> _postBuildActions = new();
    
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

    public CliAppBuilder AddPostBuildAction(Action<IHost> action)
    {
        _postBuildActions.Add(action);
        return this;
    }

    public CliApp Build()
    {
        var host = _builder.Build();
        foreach (var action in _postBuildActions)
        {
            action(host);
        }
        return new CliApp(host);
    }

    public async Task RunAsync(string[] args)
    {
        var cliApp = Build();
        await cliApp.RunAsync(args);
    }
}
