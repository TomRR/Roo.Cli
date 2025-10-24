using Spectre.Console;

namespace Roo.Cli.Commands;

public abstract class RooCommandBase : ICommand
{
    private readonly IRooLogger _logger;
    private readonly IRooConfigService _configService;
    private readonly IDirectoryService _directoryService;

    protected RooCommandBase(IRooLogger logger, IRooConfigService configService, IDirectoryService directoryService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        _directoryService = directoryService ?? throw new ArgumentNullException(nameof(directoryService));
    }

    protected async Task WithRooConfigAsync(Func<RepositoryDto, Task> repositoryAction)
    {
        var rooConfigResult = _configService.GetRooConfig();
        if (rooConfigResult.HasFailed)
        {
            _logger.Log(rooConfigResult.Error.Description);
            return;
        }
        var dto = rooConfigResult.Value.MapTo();
        // await rooConfigResult.Value.Repositories.ForEachRepositories(WrapRepositoryAction(repositoryAction));
        await dto.Repositories.ForEachRepositories(async repo =>
        {
            if (ShouldSkipRepository(repo))
            {
                return;
            }
            await repositoryAction(repo);
            _logger.Log(LoggingComponents.GreyDimRule());
        });
    }
    private Func<RepositoryDto, Task> WrapRepositoryAction(Func<RepositoryDto, Task> repositoryAction)
    {
        return async repo =>
        {
            if (ShouldSkipRepository(repo))
            {
                return;
            }
            await repositoryAction(repo);
            _logger.Log(LoggingComponents.GreyDimRule());
        };
    }

    
    protected virtual bool ShouldSkipRepository(RepositoryDto repository)
    {
        // for cloning the path isn't a repository path so this check has to be skipped
        var gitPath = Path.Combine(repository.GetLocalRepoPath(), ".git");
        if (_directoryService.DirectoryExists(gitPath))
        {
            return false;
        }   
        
        _logger.Log($"⚠️  {repository.Path} is not a git repository.");
        return true;
    }

    public abstract Task RunAsync();
}
