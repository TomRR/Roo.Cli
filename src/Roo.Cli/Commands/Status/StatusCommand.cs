using Microsoft.Extensions.Logging;
using Roo.Cli.Cli.Nuget;

namespace Roo.Cli.Commands.Status;

public class StatusCommand : ICommand
{
    private readonly ILogger<StatusCommand> _logger;
    private readonly ITest _test;

    public StatusCommand(ILogger<StatusCommand> logger, ITest test)
    {
        _logger = logger;
        _test = test;
    }

    public Task RunAsync(string[] args)
    {
        Console.WriteLine($"status + {_test.Get()}");
        _logger.LogInformation("Repository initialized.");
        return Task.CompletedTask;
    }
}