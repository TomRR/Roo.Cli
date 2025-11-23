namespace Roo.Cli.Features.Commands.Git.Status;

public enum RepoStatus
{
    Clean,
    Untracked,
    Modified,
    Staged,
    Ahead,
    Behind,
    Diverged,
    Error
}

public static class RepoStatusExtensions
{
    public static string StateToEmoji(this RepoStatus state) => state switch
    {
        RepoStatus.Clean => $"[green]{Icons.GreenDotIcon} Clean[/]",
        RepoStatus.Untracked => $"[yellow]{Icons.YellowDotIcon} Untracked[/]",
        RepoStatus.Modified => $"[yellow]{Icons.YellowDotIcon} Modified[/]",
        RepoStatus.Ahead => $"[blue]{Icons.UpArrowIcon} Ahead[/]",
        RepoStatus.Behind => $"[magenta]{Icons.BelowArrowIcon} Behind[/]",
        RepoStatus.Diverged => $"[red]️{Icons.WarningIcon} Diverged[/]",
        RepoStatus.Error => $"[red]{Icons.ErrorIcon} Error[/]",
        _ => "[grey]Unknown[/]"
    };
}