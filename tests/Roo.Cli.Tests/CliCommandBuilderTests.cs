using Roo.Cli.Features.Commands.Core;

namespace Roo.Cli.Tests;

public class CliCommandBuilderTests
{
    [Fact]
    public void Build_AddsFlagsOptionsAndPositionalsCorrectly()
    {
        var builder = CliCommandBuilder.Create()
            .Add("push")
            .AddFlag("--force", true)
            .AddFlag("--dry-run", false)           // should be ignored
            .AddOption("--set-upstream", "origin")
            .AddOption("--branch", null)           // should be ignored
            .AddPositional("main")
            .AddPositional(null);                  // should be ignored

        var result = builder.Build();

        var expected = new[] { "push", "--force", "--set-upstream", "origin", "main" };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AddRange_AddsMultipleArgs()
    {
        var builder = CliCommandBuilder.Create()
            .AddRange(new[] { "arg1", "arg2", null, "" });  // null/empty ignored

        var result = builder.Build();

        var expected = new[] { "arg1", "arg2" };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_EmptyOrWhitespace_DoesNotAdd()
    {
        var builder = CliCommandBuilder.Create()
            .Add(null)
            .Add("")
            .Add("   "); // whitespace

        var result = builder.Build();
        Assert.Empty(result);
    }

    [Fact]
    public void Build_EmptyBuilder_ReturnsEmptyList()
    {
        var builder = CliCommandBuilder.Create();
        var result = builder.Build();
        Assert.Empty(result);
    }

    [Fact]
    public void Build_ReturnsReadOnlyList()
    {
        var builder = CliCommandBuilder.Create().Add("push");
        var result = builder.Build();
        Assert.IsAssignableFrom<IReadOnlyList<string>>(result);
    }

    [Fact]
    public void AddOption_ThrowsOnInvalidName()
    {
        var builder = CliCommandBuilder.Create();
        Assert.Throws<ArgumentException>(() => builder.AddOption(null!, "value"));
        Assert.Throws<ArgumentException>(() => builder.AddOption("", "value"));
        Assert.Throws<ArgumentException>(() => builder.AddOption("  ", "value"));
    }

    [Fact]
    public void BuildAsString_ReturnsCorrectString()
    {
        var builder = CliCommandBuilder.Create()
            .Add("git")
            .AddFlag("--force")
            .AddOption("--branch", "main");

        var result = builder.BuildAsString();
        Assert.Equal("git --force --branch main", result);
    }

    [Fact]
    public void Chaining_AddMultipleFlagsAndOptions_OrderIsPreserved()
    {
        var builder = CliCommandBuilder.Create()
            .Add("push")
            .AddFlag("--force")
            .AddOption("--set-upstream", "origin")
            .AddFlag("--dry-run", true)
            .AddPositional("main");

        var result = builder.Build();
        var expected = new[] { "push", "--force", "--set-upstream", "origin", "--dry-run", "main" };
        Assert.Equal(expected, result);
    }
}