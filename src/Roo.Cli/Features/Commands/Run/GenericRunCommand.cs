namespace Roo.Cli.Features.Commands.Run;


[Command("run")]
public sealed partial class GenericRunCommand(ICommandHandler<RunRequest> handler, RooCommandContext context) : RooCommandBase(context)
{
    private readonly ICommandHandler<RunRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));

    [Argument("command", 0)]
    public string Command { get; set; } = string.Empty;

    public override async Task RunAsync()
    {
        if (string.IsNullOrWhiteSpace(Command))
        {
            _logger.LogError(Error.Validation("Please provide a command to run."));
            return;
        }

        await WithRooConfigAsync(RunCommandPerRepositoryAsync);
    }

    private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
    {
        _logger.Log($"Running '{Command}' for {repository.Name}...");
        
        var request = RunRequest.Create(CommandName, repository, Command, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);
        
        _logger.LogCommandResult(result);
    }
}
