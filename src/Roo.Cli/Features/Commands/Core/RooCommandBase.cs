namespace Roo.Cli.Features.Commands.Core;

public abstract partial class RooCommandBase : ICommand
{
    private readonly IRooLogger _logger;
    private readonly IRooConfigService _configService;
    private readonly IDirectoryService _directoryService;
    private readonly IRooCommandValidator _validator;

    protected RooCommandBase(RooCommandContext context)
    {
        _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
        _configService = context.Config ?? throw new ArgumentNullException(nameof(context.Config));
        _directoryService = context.DirectoryService ?? throw new ArgumentNullException(nameof(context.DirectoryService));
        _validator = context.Validator ?? throw new ArgumentNullException(nameof(context.Validator));
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
        await dto.Repositories.ForEachRepositories(WrapRepositoryAction(repositoryAction));
    }
    private Func<RepositoryDto, Task> WrapRepositoryAction(Func<RepositoryDto, Task> repositoryAction)
    {
        return async repo =>
        {
            if (ShouldSkipRepository(repo))
            {
                return;
            }

            var validation = _validator.IsRepositoryUrlValid(repo);
            if (validation.HasFailed)
            {
                _logger.LogError(
                    validation.Error, 
                    additionalLineBreaksAfter: 2);
                return;
            }
            

            await repositoryAction(repo);
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
        
        _logger.Log(Components.Messages.NoGitRepository(repository.GetLocalRepoPath()));
        return true;
    }

    public abstract Task RunAsync();
}
