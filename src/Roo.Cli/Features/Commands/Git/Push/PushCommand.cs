namespace Roo.Cli.Features.Commands.Git.Push;

[Command("push")]
public sealed partial class PushCommand(ICommandHandler<PushRequest> handler, RooCommandContext context)
    : RooCommandBase(context)
{

    private readonly ICommandHandler<PushRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));

    [Option("--force", "-f", hasValue: false, description: "Force push")]
    public bool Force { get; set; }

    [Option("--set-upstream", hasValue: false)]
    public bool SetUpstream { get; set; }

    [Argument(ofOption: nameof(SetUpstream), 0)]
    public string? UpstreamRemote { get; set; }

    [Argument(ofOption: nameof(SetUpstream), 1)]
    public string? UpstreamBranch { get; set; }

    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.PushCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }

    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log(Components.Messages.GetPushingWithRepoName(repository.Name));
        
        var request = PushRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}