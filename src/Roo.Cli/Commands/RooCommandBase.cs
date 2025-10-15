namespace Roo.Cli.Commands;

public abstract class RooCommandBase : ICommand
{
    private readonly IRooLogger _logger;
    private readonly IRooConfigRetriever _configRetriever;

    protected RooCommandBase(IRooLogger logger, IRooConfigRetriever configRetriever)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configRetriever = configRetriever ?? throw new ArgumentNullException(nameof(configRetriever));
    }

    protected async Task WithRooConfigAsync(Func<Repository, Task> repositoryAction)
    {
        var rooConfigResult = _configRetriever.RetrieveRooConfig();
        if (rooConfigResult.HasFailed)
        {
            _logger.Log(rooConfigResult.Error.Description);
            return;
        }

        await rooConfigResult.Value.Repositories.ForEachRepositories(repositoryAction, _logger);
    }

    public abstract Task RunAsync();
}
