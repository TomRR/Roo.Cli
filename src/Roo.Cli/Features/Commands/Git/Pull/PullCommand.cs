namespace Roo.Cli.Features.Commands.Git.Pull;

[Command("pull")]
public sealed partial class PullCommand : RooCommandBase
{
    private readonly ICommandAction<PullCommand> _action;
    private readonly IRooLogger _logger;
    public PullCommand(RooCommandContext context, ICommandAction<PullCommand> action)        
        : base(context)
    {
        _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }
    
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
        _logger.Log(Components.Rules.GreyDimRule());
    }
    private IReadOnlyList<string> BuildCommand()
    {
        var builder = CliCommandBuilder.Create()
            .Add(CommandName)
            .AddFlag("--force", Force);
        
        var args = builder.Build();
        return args;
    }
    
}