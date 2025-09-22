namespace Roo.Cli.Features.Commands.Git.Pull;

[Command("pull")]
public sealed partial class PullCommand(ICommandHandler<PullRequest> handler, RooCommandContext context)
    : RooCommandBase(context)
{

    private readonly ICommandHandler<PullRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
    
    [Option("--force", "-f", hasValue: false, description: "Force pull")]
    public bool Force { get; set; }
    
    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.PullCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }
    
    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log(Components.Messages.GetPullingWithRepoName(repository.Name));
        
        var request = PullRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}