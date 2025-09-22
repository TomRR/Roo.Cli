using Roo.Cli.Features.Commands.Git.Status.Processing;

namespace Roo.Cli.Features.Commands.Git.Status;

[Command("status")]
public sealed partial class StatusCommand(ICommandHandler<StatusRequest> handler, IGitStatusParser parser, IGitRepoStatusRenderer renderer, RooCommandContext context)
    : RooCommandBase(context)
{

    private readonly ICommandHandler<StatusRequest> _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IRooLogger _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));

    private readonly IGitStatusParser _gitStatusParser = parser ?? throw new ArgumentNullException(nameof(parser));
    private readonly IGitRepoStatusRenderer _statusRenderer = renderer ?? throw new ArgumentNullException(nameof(renderer));

    private Table? _summaryTable;
    
    
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
        var request = StatusRequest.Create(CommandName, repository, Interactive, Select, false);
        var result = await _handler.HandleAsync(request);

        var logResult = _logger.LogCommandResultErrors(result);
        if (HasLoggedErrors(logResult))
        {
            return;
        }
        
        var stdout = result.Value?.StandardOutput ?? string.Empty;
        var parsed = _gitStatusParser.Parse(stdout);

        _statusRenderer.Render(repository, parsed, HasChanges);
        
        AddSummaryTableRow(repository, parsed);
        _logger.Log(Components.Rules.GreyDimRule());
        

    }

    private static bool HasLoggedErrors(Result<NoErrors, Errors> logResult)
    {
        return logResult.HasError;
    }
    
    private void AddSummaryTableRow(RepositoryDto repository, GitRepoStatusInfo parsed)
    {
        _summaryTable?.AddRow(
            $"[grey]{repository.Name}[/]",
            $"{GetStatusSummary(parsed)} ({parsed.BranchHeadName})",
            $"{GetSyncStatus(parsed)}"
        );
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
