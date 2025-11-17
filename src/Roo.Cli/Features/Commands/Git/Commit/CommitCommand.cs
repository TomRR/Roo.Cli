namespace Roo.Cli.Features.Commands.Git.Commit;

[Command("commit")]
public sealed partial class CommitCommand(ICommandHandler<CommitRequest> handler, RooCommandContext context)
    : RooCommandBase(context)
{

    private readonly ICommandHandler<CommitRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
    
    
    [Option("--amend", hasValue: false)]
    public bool Amend { get; set; }
    
    [Option("--message", shortName: "-m", hasValue: false)]
    public bool Message { get; set; }

    [Argument(ofOption: nameof(Message), 0)]
    public string? MessageArg { get; set; }

    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.CommitCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }

    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log(Components.Messages.GetCommittingWithRepoName(repository.Name));

        var request = CommitRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}
