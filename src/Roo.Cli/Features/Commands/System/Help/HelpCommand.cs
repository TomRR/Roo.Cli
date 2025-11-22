namespace Roo.Cli.Features.Commands.System.Help;

[Command("help", description: "",  "-h", "-?")]
public sealed partial class HelpCommand  : ICommand
{
    private readonly ICommandAction<HelpCommand> _action;
    private readonly IRooLogger _logger;
    private readonly IRooPrompt _prompt;

    public HelpCommand(ICommandAction<HelpCommand> action, IRooLogger logger, IRooPrompt prompt)        
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _prompt = prompt ?? throw new ArgumentNullException(nameof(prompt));
    }
    
    public async Task RunAsync() => await RunHelpCommandAsync();
    
    private async Task RunHelpCommandAsync()
    {
        _logger.Log("Help");

        // _promptHandler.AskYesNo("what");
        // _prompt.MultiSelect("", new[] {""});
        
        
        _prompt.AskYesNo("what");

        // _logger.Log(s.ToString());

        // _promptHandler.SingleSelect();
        // _promptHandler.MultiSelect();
        
        await Task.CompletedTask;
    }
}