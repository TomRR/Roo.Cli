namespace Roo.Cli.Tests;

public class GitRepoStatusRendererTests
{
    private readonly GitRepoStatusRenderer _sut;
    private readonly IRooLogger _logger;
    
    public GitRepoStatusRendererTests()
    {
        _sut = new GitRepoStatusRenderer(new TestLogger());
    }
    [Fact]
    public void Render_ShouldPrint_AllFileCategories()
    {
        // Arrange
        var parsed = new GitRepoStatusInfo
        {
            BranchHeadName = "main",
            BranchUpstreamName = "origin/main",
            Ahead = 2,
            Behind = 1,
        };

        parsed.StagedFiles.Add("src/Staged.cs");
        parsed.ModifiedFiles.Add("src/Modified.cs");
        parsed.DeletedFiles.Add("src/Deleted.cs");
        parsed.UntrackedFiles.Add("src/NewFile.cs");
        parsed.IgnoredFiles.Add("bin/temp.dll");
        parsed.ConflictedFiles.Add("src/Conflict.cs");

        // Act
        _sut.Render(parsed);

        // Assert
    }

    [Fact]
    public void Render_ShouldPrint_CleanMessage_WhenNoChanges()
    {
        // Arrange
        var parsed = new GitRepoStatusInfo
        {
            BranchHeadName = "main",
            BranchUpstreamName = "origin/main"
        };

        // Act
        _sut.Render(parsed);
    }

    [Fact]
    public void Render_ShouldPrint_IgnoredFiles_WhenPresent()
    {
        // Arrange
        var parsed = new GitRepoStatusInfo
        {
            BranchHeadName = "dev",
            BranchUpstreamName = "origin/dev"
        };
        parsed.IgnoredFiles.Add("obj/temp.cache");

        // Act
        _sut.Render(parsed);
    }
}
