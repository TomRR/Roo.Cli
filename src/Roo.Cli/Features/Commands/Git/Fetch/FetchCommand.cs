namespace Roo.Cli.Features.Commands.Git.Fetch;

[Command("fetch")]
public sealed partial class FetchCommand(ICommandHandler<FetchRequest> handler, RooCommandContext context)
    : RooCommandBase(context)
{

    private readonly ICommandHandler<FetchRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));

    [Option("--force", "-f", hasValue: false, description: "Force pull")]
    public bool Force { get; set; }
    
    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.FetchCommandRule());
        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }
    
    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log(Components.Messages.GetFetchingWithRepoName(repository.Name));
        
        var request = FetchRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}