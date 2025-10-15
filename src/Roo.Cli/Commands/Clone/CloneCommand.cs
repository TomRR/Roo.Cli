namespace Roo.Cli.Commands.Clone;

[Command("clone")]
public class CloneCommand : RooCommandBase
{
    private readonly ICommandAction<StatusCommand> _action;
    private readonly IRooLogger _logger;
    public CloneCommand(ICommandAction<StatusCommand> action, IRooLogger logger, IRooConfigRetriever configRetriever)        
        : base(logger, configRetriever)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    
    public override async Task RunAsync()
    {
        await WithRooConfigAsync(RunStatusCommandAsync);
    }
    private async Task RunStatusCommandAsync(Repository repository)
    {
        var commandResult = await _action.RunCommandAsync(repository);
        
        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            return;
        }
        _logger.Log($"Command succeeded: {commandResult.Value.IsSuccess}");
        _logger.Log(ConsoleLogHelper.Output);
    }
}