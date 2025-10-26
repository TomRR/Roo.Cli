using Cli.Toolkit.SourceGenerators.CommandBinders;
using Roo.Cli.Features.Commands.Core;
using Roo.Cli.Features.Commands.Git.Push;
using Roo.Cli.Infrastructure.Config;
using Roo.Cli.Infrastructure.IO;
using Roo.Cli.Infrastructure.Logging;

namespace Roo.Cli.Tests;

public class PushCommandBinderTests
{
    private static PushCommand CreateCommand()
    {
        // --- Create substitutes ---
        var logger = Substitute.For<IRooLogger>();
        var config = Substitute.For<IRooConfigService>();
        var directory = Substitute.For<IDirectoryService>();
        var validator = Substitute.For<IRooCommandValidator>();
        var action = Substitute.For<ICommandAction<PushCommand>>();

        // --- Build the context ---
        var context = new RooCommandContext(logger, config, directory, validator);

        // --- Create the command instance ---
        return new PushCommand(context, action);
    }

    [Fact]
    public void Bind_SetsInteractiveFlag()
    {
        var cmd = CreateCommand();
        var args = new[] { "--interactive" };

        PushCommand_Binder.Bind(cmd, args);

        Assert.True(cmd.Interactive);
        Assert.False(cmd.Force);
        Assert.False(cmd.Select);
    }

    [Fact]
    public void Bind_SetsForceFlag_ShortOption()
    {
        var cmd = CreateCommand();

        PushCommand_Binder.Bind(cmd, new[] { "-f" });

        Assert.True(cmd.Force);
    }

    [Fact]
    public void Bind_SetsSelectOption_WithRepository()
    {
        var cmd = CreateCommand();

        PushCommand_Binder.Bind(cmd, new[] { "--select", "MyRepo" });

        Assert.True(cmd.Select);
        Assert.Equal("MyRepo", cmd.SelectedRepositoryName);
    }

    [Fact]
    public void Bind_SetsUpstream_WithRemoteAndBranch()
    {
        var cmd = CreateCommand();

        PushCommand_Binder.Bind(cmd, new[] { "--set-upstream", "origin", "main" });

        Assert.True(cmd.SetUpstream);
        Assert.Equal("origin", cmd.UpstreamRemote);
        Assert.Equal("main", cmd.UpstreamBranch);
    }

    [Fact]
    public void Bind_HandlesMissingArguments_Gracefully()
    {
        var cmd = CreateCommand();

        PushCommand_Binder.Bind(cmd, new[] { "--set-upstream", "origin" });

        Assert.True(cmd.SetUpstream);
        Assert.Equal("origin", cmd.UpstreamRemote);
        Assert.Null(cmd.UpstreamBranch);
    }

    [Fact]
    public void Bind_IgnoresUnknownOptions()
    {
        var cmd = CreateCommand();

        PushCommand_Binder.Bind(cmd, new[] { "--unknown", "--interactive" });

        Assert.True(cmd.Interactive);
        Assert.False(cmd.Force);
    }
}
