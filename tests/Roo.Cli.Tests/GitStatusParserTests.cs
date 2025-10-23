namespace Roo.Cli.Tests;

public class GitStatusParserTests
{
    private readonly GitStatusParser _parser = new();

    [Fact]
    public void Parse_ShouldDetectModifiedFile()
    {
        string output = "1 .M N... 100644 100644 100644 hash1 hash2 src/file.cs";
        var result = _parser.Parse(output);
        Assert.Equal(RepoStatus.Modified, result.Status);
        Assert.Contains("src/file.cs", result.ModifiedFiles);
    }

    [Fact]
    public void Parse_ShouldDetectStagedAddedFile()
    {
        string output = "1 A. N... 100644 100644 100644 hash1 hash2 src/newfile.cs";
        var result = _parser.Parse(output);
        Assert.Equal(RepoStatus.Staged, result.Status);
        Assert.Contains("src/newfile.cs", result.StagedFiles);
    }

    [Fact]
    public void Parse_ShouldDetectDeletedFile()
    {
        string output = "1 .D N... 100644 100644 000000 hash1 hash2 src/deleted.cs";
        var result = _parser.Parse(output);
        Assert.Equal(RepoStatus.Modified, result.Status);
        Assert.Contains("src/deleted.cs", result.DeletedFiles);
    }

    [Fact]
    public void Parse_ShouldDetectUntrackedFile()
    {
        string output = "? src/newdir/";
        var result = _parser.Parse(output);
        Assert.Equal(RepoStatus.Untracked, result.Status);
        Assert.Contains("src/newdir/", result.UntrackedFiles);
    }

    [Fact]
    public void Parse_ShouldDetectConflictedFile()
    {
        string output = "1 UU N... 100644 100644 100644 hash1 hash2 src/conflict.cs";
        var result = _parser.Parse(output);
        Assert.Equal(RepoStatus.Error, result.Status);
        Assert.Contains("src/conflict.cs", result.ConflictedFiles);
    }

    [Fact]
    public void Parse_ShouldDetectAheadBehindStatus()
    {
        string output = "# branch.ab +2 -1";
        var result = _parser.Parse(output);
        Assert.Equal(RepoStatus.Diverged, result.Status);
        Assert.Equal(2, result.Ahead);
        Assert.Equal(1, result.Behind);
    }
}