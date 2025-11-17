using Roo.Cli.Features.Commands.Core;
using Roo.Cli.Features.Commands.Git.Status;
using Roo.Cli.Features.Commands.Git.Status.Models;
using Roo.Cli.Features.Commands.Git.Status.Processing;

namespace Roo.Cli.Tests;



public class GitRepoStatusFormatterTests
{
    private RepositoryDto TestRepositoryDto =  RepositoryDtoFactory.Create(url: "/tmp/testrepo");

    private GitRepoStatusInfo CreateStatusInfo(
        bool clean = false,
        int ahead = 0,
        int behind = 0)
    {
        var info = new GitRepoStatusInfo
        {
            BranchHeadName = "main",
            BranchUpstreamName = "origin/main",
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

    // private GitRepoStatusInfo CreateStatusInfo(
    //     bool clean = false,
    //     int ahead = 0,
    //     int behind = 0)
    //     => new GitRepoStatusInfo
    //     {
    //         BranchHeadName = "main",
    //         BranchUpstreamName = "origin/main",
    //         StagedFiles = clean ? new() : new() { "file1.cs" },
    //         ModifiedFiles = clean ? new() : new() { "Program.cs" },
    //         DeletedFiles = clean ? new() : new(),
    //         UntrackedFiles = clean ? new() : new(),
    //         IgnoredFiles = new(),
    //         ConflictedFiles = new(),
    //         Ahead = ahead,
    //         Behind = behind
    //     };

    [Fact]
    public void Format_ReturnsCleanMessage_WhenRepoIsClean()
    {
        var status = CreateStatusInfo(clean: true);

        var result = GitRepoStatusFormatter.Format(TestRepositoryDto, status);

        Assert.Contains("Repository is clean", result);
        Assert.Contains("Branch:", result);
    }

    [Fact]
    public void Format_ReturnsEmpty_WhenCleanAndOnlyIfChangesIsTrue()
    {
        var status = CreateStatusInfo(clean: true);

        var result = GitRepoStatusFormatter.Format(TestRepositoryDto, status, onlyIfChanges: true);

        Assert.True(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void Format_ContainsAllSectionTitles_WhenRepoHasChanges()
    {
        var status = CreateStatusInfo(clean: false);

        var result = GitRepoStatusFormatter.Format(TestRepositoryDto, status);

        Assert.Contains("Staged files", result);
        Assert.Contains("Modified files", result);
        Assert.DoesNotContain("Repository is clean", result);
    }

    [Fact]
    public void Format_IncludesAheadBehind_WhenSet()
    {
        var status = CreateStatusInfo(clean: false, ahead: 2, behind: 1);

        var result = GitRepoStatusFormatter.Format(TestRepositoryDto, status);

        Assert.Contains("ahead", result);
        Assert.Contains("behind", result);
    }

    [Fact]
    public void Format_ReturnsExpectedBranchLine()
    {
        var status = CreateStatusInfo(clean: true);

        var result = GitRepoStatusFormatter.Format(TestRepositoryDto, status);

        Assert.Contains("Branch:", result);
        Assert.Contains("Head: main", result);
        Assert.Contains("origin/main", result);
    }
}