namespace Roo.Cli.Features.Commands.System.Help;

[Command("help", description: "",  "-h", "-?")]
public sealed partial class HelpCommand  : ICommand
{
    private readonly ICommandAction<HelpCommand> _action;
    private readonly IRooLogger _logger;
    public HelpCommand(ICommandAction<HelpCommand> action, IRooLogger logger)        
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task RunAsync() => await RunHelpCommandAsync();
    
    private async Task RunHelpCommandAsync()
    {
        _logger.Log("Help");

        await Task.CompletedTask;
    }
}