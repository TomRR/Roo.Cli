namespace Roo.Cli.Features.Commands.Git.Push;

[Command("push")]
public sealed partial class PushCommand : RooCommandBase
{

    private readonly ICommandAction<PushCommand> _action;
    private readonly IRooLogger _logger;

    public PushCommand(RooCommandContext context, ICommandAction<PushCommand> action)        
        : base(context)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
    }

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

        var args = BuildCommand();

        var request = RooCommandRequest.Create(repository, args);
        var commandResult = await _action.RunCommandAsync(request);

        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            _logger.Log(Components.Rules.GreyDimRule());
            return;
        }

        var output = string.IsNullOrWhiteSpace(commandResult.Value.StandardOutput)
            ? $"{Icons.GreenDotIcon} Everything up-to-date"
            : $"{Icons.GreenDotIcon} {commandResult.Value.StandardOutput}";
        _logger.Log(output);
        _logger.Log(Components.Rules.GreyDimRule());
    }

    private IReadOnlyList<string> BuildCommand()
    {
        var builder = CliCommandBuilder.Create()
            .Add(CommandName)
            .AddFlag("--interactive", Interactive)
            .AddFlag("--force", Force);

        if (SetUpstream)
        {
            builder.AddFlag("--set-upstream");

            if (!string.IsNullOrWhiteSpace(UpstreamRemote))
                builder.AddPositional(UpstreamRemote);

            if (!string.IsNullOrWhiteSpace(UpstreamBranch))
                builder.AddPositional(UpstreamBranch);
        }

        return builder.Build();
    }
}