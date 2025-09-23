using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Roo.Cli.Commands.Status;

[Command("status")]
public class StatusCommand : ICommand
{
    private readonly ILogger<StatusCommand> _logger;

    public StatusCommand(ILogger<StatusCommand> logger)
    {
        _logger = logger;
    }

    public Task RunAsync()
    {
        _logger.LogInformation("Repository initialized.");
        AnsiConsole.Write(new Markup("[bold yellow]Hello[/] [red]World![/]"));

        return Task.CompletedTask;
    }
}