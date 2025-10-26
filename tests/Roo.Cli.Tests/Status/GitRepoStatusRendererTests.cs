using Roo.Cli.Features.Commands.Core;
using Roo.Cli.Features.Commands.Git.Status;
using Roo.Cli.Infrastructure.Logging;

namespace Roo.Cli.Tests.Status;

public class GitRepoStatusRendererTests
{
    private readonly GitRepoStatusRenderer _sut;

    public GitRepoStatusRendererTests()
    {
        var logger = Substitute.For<IRooLogger>();
        _sut = new GitRepoStatusRenderer(logger);

    }
    private RepositoryDto TestRepositoryDto =  RepositoryDtoFactory.Create(url: "/tmp/testrepo");
    
    private GitRepoStatusInfo CleanStatus = GitRepoStatusInfoFactory.Create(clean: true);

    private GitRepoStatusInfo DirtyStatus = GitRepoStatusInfoFactory.Create(
        clean: false,
        ahead: 2,
        behind: 1,
        branchHead: "develop",
        branchUpstream: "origin/develop"
    );

    // [Fact]
    // public void Render_LogsFormattedOutput_WhenRepoHasChanges()
    // {
    //     _sut.Render(TestRepositoryDto, DirtyStatus);
    //
    //     Assert.NotEmpty(logger.Messages);
    //     Assert.Contains("Staged files", logger.Messages[0]);
    //     Assert.Contains("Branch:", logger.Messages[0]);
    // }
    //
    // [Fact]
    // public void Render_DoesNotLog_WhenCleanAndOnlyIfChangesIsTrue()
    // {
    //     _sut.Render(TestRepositoryDto, CleanStatus, onlyIfChanges: true);
    //
    //     Assert.Empty(logger.Messages);
    // }
}