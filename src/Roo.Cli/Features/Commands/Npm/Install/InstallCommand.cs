namespace Roo.Cli.Features.Commands.Npm.Install;

[Command("install")]
public sealed partial class InstallCommand(ICommandHandler<InstallRequest> handler, RooCommandContext context) : RooCommandBase(context)
{
    private readonly ICommandHandler<InstallRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));

    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.NpmInstallCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }

    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log($"Running npm install for {repository.Name}...");
        
        var request = InstallRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}
