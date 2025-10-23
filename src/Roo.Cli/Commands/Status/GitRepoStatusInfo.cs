namespace Roo.Cli.Commands.Status;

public class GitRepoStatusInfo
{
    public string BranchHeadName { get; set; } = string.Empty;
    public string BranchUpstreamName { get; set; } = string.Empty;
    public RepoStatus Status { get; set; } = RepoStatus.Clean;
    public List<string> ModifiedFiles { get; set; } = new();
    public List<string> UntrackedFiles { get; set; } = new();
    public int Ahead { get; set; }
    public int Behind { get; set; }
}