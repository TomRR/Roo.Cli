using Spectre.Console;

namespace Roo.Cli.Commands.Status;

[Command("status")]
public class StatusCommand : RooCommandBase
{
    private readonly IRooLogger _logger;
    private readonly IGitStatusParser _gitStatusParser;
    private readonly IGitRepoStatusRenderer _statusRenderer;
    private readonly ICommandAction<StatusCommand> _action;
    private Table _table;

    public StatusCommand(
        ICommandAction<StatusCommand> action,
        IRooLogger logger,
        IRooConfigService configService,
        IDirectoryService directoryService,
        IGitStatusParser gitStatusParser,
        IGitRepoStatusRenderer renderer)
        : base(logger, configService, directoryService)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _gitStatusParser = gitStatusParser ?? throw new ArgumentNullException(nameof(gitStatusParser));
        _statusRenderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
    }

    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }

    public override async Task RunAsync()
    {
        _table = new Table()
            .RoundedBorder()
            .Title($"[bold cyan]{Icons.PackageIcon} Roo Status Summary[/]")
            .AddColumn("[bold]Repository[/]")
            .AddColumn("[bold]Status[/]")
            .AddColumn("[bold]Sync[/]");

        _logger.Log(LoggingComponents.GetStatusCommandRule());
        await WithRooConfigAsync(RunStatusCommandAsync);
        _logger.Log(_table);
    }

    private async Task RunStatusCommandAsync(RepositoryDto repository)
    {
        _logger.Log(LoggingComponents.GetRepoName(repository.Name));

        var commandResult = await _action.RunCommandAsync(repository);

        if (commandResult.HasError)
        {
            _logger.LogError(commandResult.Error);
            return;
        }

        var parsed = _gitStatusParser.Parse(commandResult.Value.StandardOutput);

        _table.AddRow(
            $"[grey]{repository.Name}[/]",
            $"{GetStatusSummary(parsed)} ({parsed.BranchHeadName})",
            $"{GetSyncStatus(parsed)}"
        );

        _statusRenderer.Render(parsed);
        
        _logger.Log(LoggingComponents.GreyDimRule());
    }

    private static string GetStatusSummary(GitRepoStatusInfo info)
    {
        return info.Status switch
        {
            RepoStatus.Clean => "🟢 Clean",
            RepoStatus.Error => "🔴 Conflict",
            _ when info.StagedFiles.Any() || info.ModifiedFiles.Any() ||
                   info.DeletedFiles.Any() || info.UntrackedFiles.Any() => "🟠 Changes",
            _ => "🟡 Unknown"
        };
    }
    private string GetSyncStatus(GitRepoStatusInfo parsed) => (parsed.Ahead, parsed.Behind) switch
    {
        (0, 0) => "In [orange1]sync[/]",
        (>0, >0) => $"{Icons.ArrowUpSimpleIcon}{parsed.Ahead}/{Icons.ArrowDownSimpleIcon}{parsed.Behind}",
        (>0, 0) => $"{Icons.ArrowUpSimpleIcon} {parsed.Ahead} ahead",
        (0, >0) => $"{Icons.ArrowDownSimpleIcon} {parsed.Behind} behind",
        _ => "Unknown"
    };
}
