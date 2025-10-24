namespace Roo.Cli.Commands.Fetch;

[Command("fetch")]
public class FetchCommand : RooCommandBase
{
    private readonly ICommandAction<FetchCommand> _action;
    private readonly IRooLogger _logger;
    public FetchCommand(ICommandAction<FetchCommand> action, IRooLogger logger, IRooConfigService configService, IDirectoryService  directoryService)        
        : base(logger, configService, directoryService)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    
    [Option("--force", "-f", hasValue: false, description: "Force pull")]
    public bool Force { get; set; }
    
    public override async Task RunAsync()
    {
        _logger.Log(LoggingComponents.FetchCommandRule());

        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }
    
    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        if (string.IsNullOrWhiteSpace(repository.Url))
        {
            _logger.LogError(
                LoggingComponents.GetRepoUrlMissingError(repository.Name), 
                additionalLineBreaksAfter: 1);
            return;
        }
        
        _logger.Log(LoggingComponents.GetFetchingWithRepoName(repository.Name));
        var commandResult = await _action.RunCommandAsync(repository);
        
        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            return;
        }
        
        _logger.Log($"{Icons.CheckIcon} DONE");
    }
    
}