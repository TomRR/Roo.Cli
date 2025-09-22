using Microsoft.Extensions.Hosting;

namespace Roo.Cli.Cli.Nuget;

public class CliApp
{
    private readonly IHost _host;

    internal CliApp(IHost host)
    {
        _host = host;
    }

    public async Task RunAsync(string[] args)
    {
        var dispatcher = _host.Services.GetRequiredService<CommandDispatcher>();
        await dispatcher.DispatchAsync(args);
    }

    public void Run(string[] args) =>
        RunAsync(args).GetAwaiter().GetResult();
}