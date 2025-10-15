namespace Roo.Cli.Commands.Help;

[Command("--help", "-h", "-?")]
public class HelpCommand  : RooCommandBase
{
    private readonly ICommandAction<HelpCommand> _action;
    private readonly IRooLogger _logger;
    public HelpCommand(ICommandAction<HelpCommand> action, IRooLogger logger, IRooConfigRetriever configRetriever)        
        : base(logger, configRetriever)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public override async Task RunAsync()
    {
        await WithRooConfigAsync(RunStatusCommandAsync);
    }
    private async Task RunStatusCommandAsync(Repository repository)
    {
        _logger.Log($"Help");

        // var commandResult = await _action.RunCommandAsync(repository);
        //
        // _logger.Log($"Command succeeded: {commandResult.IsSuccess}");
        // _logger.Log(ConsoleLogHelper.Output);
    }
}