namespace Roo.Cli.Features.Commands.Git.Commit;

[Command("commit")]
public sealed partial class CommitCommand : RooCommandBase
{
    private readonly ICommandAction<CommitCommand> _action;
    private readonly IRooLogger _logger;

    public CommitCommand(RooCommandContext context, ICommandAction<CommitCommand> action)
        : base(context)
    {
        _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }
    

    
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

        var args = BuildCommand();
        var request = RooCommandRequest.Create(repository, args);
        var commandResult = await _action.RunCommandAsync(request);

        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            _logger.Log(Components.Rules.GreyDimRule());
            return;
        }

        _logger.Log($"{Icons.GreenDotIcon} {commandResult.Value.StandardOutput}");
        _logger.Log($"{Icons.ErrorIcon} {commandResult.Value.StandardError}");
        _logger.Log(Components.Rules.GreyDimRule());
    }
    
    private IReadOnlyList<string> BuildCommand()
    {
        var builder = CliCommandBuilder.Create()
            .Add(CommandName)
            .AddFlag("--amend", Amend);

        if (Message)
        {
            builder.AddFlag("--message");

            if (!string.IsNullOrWhiteSpace(MessageArg))
            {
                builder.AddPositional(MessageArg);
            }
        }
        
        return builder.Build();
    }
}
