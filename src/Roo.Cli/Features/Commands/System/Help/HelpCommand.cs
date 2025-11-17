namespace Roo.Cli.Features.Commands.System.Help;

[Command("help", description: "",  "-h", "-?")]
public sealed partial class HelpCommand  : ICommand
{
    private readonly ICommandAction<HelpCommand> _action;
    private readonly IRooLogger _logger;
    private readonly IPromptWrapper _promptWrapper;

    public HelpCommand(ICommandAction<HelpCommand> action, IRooLogger logger, IPromptWrapper promptWrapper)        
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _promptWrapper = promptWrapper ?? throw new ArgumentNullException(nameof(promptWrapper));
    }
    
    public async Task RunAsync() => await RunHelpCommandAsync();
    
    private async Task RunHelpCommandAsync()
    {
        _logger.Log("Help");

        // _promptHandler.AskYesNo("what");
        _promptWrapper.MultiSelect("", new[] {""});
        // _logger.Log(s.ToString());

        // _promptHandler.SingleSelect();
        // _promptHandler.MultiSelect();
        
        await Task.CompletedTask;
    }
}