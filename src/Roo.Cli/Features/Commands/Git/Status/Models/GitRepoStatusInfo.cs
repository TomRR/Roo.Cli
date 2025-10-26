namespace Roo.Cli.Features.Commands.Git.Status;

public class GitRepoStatusInfo
{
    public string BranchHeadName { get; set; } = string.Empty;
    public string BranchUpstreamName { get; set; } = string.Empty;
    public int Ahead { get; set; }
    public int Behind { get; set; }

    public List<string> ModifiedFiles { get; } = new();
    public List<string> StagedFiles { get; } = new();
    public List<string> DeletedFiles { get; } = new();
    public List<string> UntrackedFiles { get; } = new();
    public List<string> IgnoredFiles { get; } = new();
    public List<string> ConflictedFiles { get; } = new();
    public RepoStatus Status { get; set; } = RepoStatus.Clean;
}

public static class GitRepoStatusInfoExtensions
{
    public static bool IsClean(this GitRepoStatusInfo info)
        => info.StagedFiles.Count == 0 &&
           info.ModifiedFiles.Count == 0 &&
           info.DeletedFiles.Count == 0 &&
           info.UntrackedFiles.Count == 0 &&
           info.IgnoredFiles.Count == 0 &&
           info.ConflictedFiles.Count == 0;
}