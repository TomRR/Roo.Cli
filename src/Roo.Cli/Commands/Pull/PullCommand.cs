using Spectre.Console;

namespace Roo.Cli.Commands.Clone;

[Command("clone")]
public class PullCommand : RooCommandBase
{
    private readonly ICommandAction<CloneCommand> _action;
    private readonly IRooLogger _logger;
    private readonly IDirectoryService _directoryService;
    private readonly IPromptHandler _promptHandler;
    private readonly List<CommandResultType> _results = new();
    public CloneCommand(ICommandAction<CloneCommand> action, IRooLogger logger, IRooConfigService configService, IDirectoryService  directoryService, IPromptHandler promptHandler)        
        : base(logger, configService, directoryService)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
        _promptHandler = promptHandler ?? throw new ArgumentNullException(nameof(promptHandler));
    }
    
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    
    [Option("--force", "-f", hasValue: false)]
    // Description = "Force overwrite existing repositories."
    public bool Force { get; set; }
    [Option("--clear", "-c", hasValue: false)]
    // Description = "clear paths before cloning"
    public bool Clear { get; set; }
    
    protected override bool ShouldSkipRepository(RepositoryDto repository) => false;    

    public override async Task RunAsync()
    {
        _logger.LogApplicationNameFiglet();
        _logger.Log(LoggingComponents.GetCloneCommandRule());
        
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
        
        _logger.AddLineBreak();
        _logger.Log(LoggingComponents.GetCloneStatisticRule());
        _logger.Log(LoggingComponents.GetCloningResultPanel(_results));
        _logger.AddLineBreak();
        _logger.LogTaskCompleted();
    }
    
    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        if (string.IsNullOrWhiteSpace(repository.Url))
        {
            _logger.LogError(
                LoggingComponents.GetRepoUrlMissingError(repository.Name), 
                additionalLineBreaksAfter: 2);
            return;
        }
        
        _logger.Log(LoggingComponents.GetRepoName(repository.Name));
        _logger.Log(LoggingComponents.GetFolderPathPanel(repository.Path));

        var existingRepoResult = CheckForExistingRepository(repository);
        if (existingRepoResult.HasError)
        {
            var additionalMessage = $"{Icons.ErrorIcon}  [red]Something went wrong with {repository.Name}[/]";
            _logger.LogError(existingRepoResult.Error, additionalMessage: additionalMessage, additionalLineBreaksAfter: 2);
            _results.Add(CommandResultType.Failed);
            return;
        }

        if (existingRepoResult  is { IsStatusOnly: true, StatusOnly: Skipped})
        {
            _logger.Log(LoggingComponents.RepoSkipped(repository.Name), additionalLineBreaksAfter: 1);
            _results.Add(CommandResultType.Skipped);
            return;
        }
        
        
        _logger.Log($"{Icons.RocketIcon} Cloning {repository.Url}...");
        var commandResult = await _action.RunCommandAsync(repository);
        
        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error, additionalLineBreaksAfter: 1);
            _results.Add(CommandResultType.Failed);
            return;
        }
        
        _logger.Log($"{Icons.GreenDotIcon} successful cloned repo '{repository.Name}' to '{repository.Path}'", additionalLineBreaksAfter: 1);
        _results.Add(CommandResultType.Success);
    }
    
    private Result<Success, Error, Skipped> CheckForExistingRepository(RepositoryDto repository)
    {
        if (!_directoryService.DirectoryExists(repository.GetLocalRepoPath()))
        {
            return Result.Success;
        }
        
        if (!Force)
        {
            var message = $"{Icons.WarningIcon} Path already exists\n Overwrite?";
            var overwriteAnswer = _promptHandler.PromptYesNo(message);
            
            if (overwriteAnswer == PromptAnswer.No)
            {
                return Result.Skipped;
            }
        
        }
        
        _logger.Log(LoggingComponents.DeletingExistingPath(repository.GetLocalRepoPath()));
        
        var deleted = _directoryService.DeleteDirectory(repository.GetLocalRepoPath(), recursive: true);
        return deleted.IsSuccessful ? deleted.Value : deleted.Error;
    }
}