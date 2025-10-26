namespace Roo.Cli.Features.Commands.Git.Fetch;

[Command("fetch")]
public sealed partial class FetchCommand(RooCommandContext context, ICommandAction<FetchCommand> action)
    : RooCommandBase(context)
{
    private readonly ICommandAction<FetchCommand> _action = action ?? throw new ArgumentNullException(nameof(action));
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

        var args = CliCommandBuilder.Create().Add(CommandName).Build();
        var request = RooCommandRequest.Create(repository, args);
        var result = await _action.RunCommandAsync(request);
        
        _logger.LogCommandResult(result);
    }
}