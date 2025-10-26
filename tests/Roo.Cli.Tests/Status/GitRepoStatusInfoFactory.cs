using Roo.Cli.Features.Commands.Git.Status;

namespace Roo.Cli.Tests.Status;

public static class GitRepoStatusInfoFactory
{
    public static GitRepoStatusInfo Create(
        bool clean = false,
        int ahead = 0,
        int behind = 0,
        string branchHead = "main",
        string branchUpstream = "origin/main")
    {
        var info = new GitRepoStatusInfo
        {
            BranchHeadName = branchHead,
            BranchUpstreamName = branchUpstream,
            Ahead = ahead,
            Behind = behind
        };

        if (!clean)
        {
            info.StagedFiles.Add("file1.cs");
            info.ModifiedFiles.Add("Program.cs");
        }

        return info;
    }
}