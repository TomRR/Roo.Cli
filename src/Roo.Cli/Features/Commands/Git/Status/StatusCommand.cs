namespace Roo.Cli.Features.Commands.Git.Status;

[Command("status")]
public sealed partial class StatusCommand : RooCommandBase
{
    private readonly IRooLogger _logger;
    private readonly IGitStatusParser _gitStatusParser;
    private readonly IGitRepoStatusRenderer _statusRenderer;
    private readonly ICommandAction<StatusCommand> _action;
    private Table? _summaryTable;

    public StatusCommand(
        RooCommandContext context,
        ICommandAction<StatusCommand> action,
        IGitStatusParser gitStatusParser,
        IGitRepoStatusRenderer renderer)
        : base(context)
    {
        _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _gitStatusParser = gitStatusParser ?? throw new ArgumentNullException(nameof(gitStatusParser));
        _statusRenderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
    }
    
    [Option("--summary", "-sum", hasValue: false, description: "print summary table")]
    public bool Summary { get; set; }
    [Option("--changed", "-ch", hasValue: false, description: "print only the repos with changed status")]
    public bool HasChanges { get; set; }

    public override async Task RunAsync()
    {
        _logger.Log(Components.Rules.StatusCommandRule());

        _summaryTable = new Table()
            .RoundedBorder()
            .Title($"[bold cyan]Roo Status Summary[/]")
            .AddColumn("[bold]Repository[/]")
            .AddColumn("[bold]Status[/]")
            .AddColumn("[bold]Sync[/]");

        await WithRooConfigAsync(RunStatusCommandAsync);
        if (Summary)
        {
            _logger.Log(_summaryTable);
        }
    }

    private async Task RunStatusCommandAsync(RepositoryDto repository)
    {
        var args = BuildCommand();
        var request = RooCommandRequest.Create(repository, args);
        var result = await _action.RunCommandAsync(request);

        if (result.HasError)
        {
            _logger.LogError(result.Error);
            _logger.Log(Components.Rules.GreyDimRule());
            return;
        }

        var stdErr = result.Value?.StandardError ?? string.Empty;
        var stdout = result.Value?.StandardOutput ?? string.Empty;
        
        if (!string.IsNullOrWhiteSpace(stdErr))
        {
            _logger.Log($"{Icons.RedDotIcon} {result.Value?.StandardError}");
            return;
        }
        
        

        var parsed = _gitStatusParser.Parse(stdout);
        
        _summaryTable?.AddRow(
            $"[grey]{repository.Name}[/]",
            $"{GetStatusSummary(parsed)} ({parsed.BranchHeadName})",
            $"{GetSyncStatus(parsed)}"
        );

        _statusRenderer.Render(repository, parsed, HasChanges);
        _logger.Log(Components.Rules.GreyDimRule());

    }
    
    private IReadOnlyList<string> BuildCommand() 
        => CliCommandBuilder.Create()
            .Add(CommandName)
            .Add("--porcelain=v2")
            .Add("--branch").Build();

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
