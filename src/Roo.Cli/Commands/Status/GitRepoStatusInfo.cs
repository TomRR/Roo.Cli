namespace Roo.Cli.Commands.Status;

// public class GitRepoStatusInfo
// {
//     public string BranchHeadName { get; set; } = string.Empty;
//     public string BranchUpstreamName { get; set; } = string.Empty;
//     public RepoStatus Status { get; set; } = RepoStatus.Clean;
//     public List<string> ModifiedFiles { get; set; } = new();
//     public List<string> UntrackedFiles { get; set; } = new();
//     public int Ahead { get; set; }
//     public int Behind { get; set; }
// }

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
