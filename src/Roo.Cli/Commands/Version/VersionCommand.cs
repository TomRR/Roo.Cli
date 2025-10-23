namespace Roo.Cli.Commands.Version;

[Command("version")]
public class VersionCommand :  ICommand
{
    private readonly IRooLogger _logger;

    public VersionCommand(IRooLogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task RunAsync()
    {
        _logger.Log("Clone");

        _logger.Log("VersionInfo:" + VersionInfo.Version);
        return Task.CompletedTask;
    }
}