using Spectre.Console;

namespace Roo.Cli.Commands.Status;

public interface IGitRepoStatusRenderer
{
    void Render(GitRepoStatusInfo parsed);
}

public class GitRepoStatusRenderer : IGitRepoStatusRenderer
{
    private readonly IRooLogger _logger;

    public GitRepoStatusRenderer(IRooLogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Render(GitRepoStatusInfo parsed)
    {
        _logger.Log($"{Icons.BranchIcon} [yellow]Branch:[/] Head: {parsed.BranchHeadName} {Icons.ArrowRightSimpleIcon} {parsed.BranchUpstreamName}");

        if (parsed.IsClean())
        {
            _logger.Log("[green]Repository is clean[/]");
            return;
        }
        
        PrintSection("[green]Staged files:[/]", parsed.StagedFiles);
        PrintSection("[orange1]Modified files:[/]", parsed.ModifiedFiles);
        PrintSection("[red]Deleted files:[/]", parsed.DeletedFiles);
        PrintSection("[yellow]Untracked files:[/]", parsed.UntrackedFiles);
        PrintSection("[grey]Ignored files:[/]", parsed.IgnoredFiles);
        PrintSection($"[red]{Icons.WarningIcon} Conflicted files:[/]", parsed.ConflictedFiles);

        if (parsed.Ahead > 0 || parsed.Behind > 0)
        {
            _logger.Log($"[blue]{Icons.ArrowUpSimpleIcon}{parsed.Ahead} ahead, {Icons.ArrowDownSimpleIcon}{parsed.Behind} behind[/]");
        }
    }

    private void PrintSection(string title, List<string> files)
    {
        if (!files.Any())
        {
            return;
        }
        _logger.Log(title);
        foreach (var file in files)
        {
            _logger.Log($"  [grey]- {file}[/]");
        }    
    }
}