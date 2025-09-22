namespace Roo.Cli.Features.Commands.Core;

public static class CliWrapFactory
{
    public static Command RunScriptCliCommand(string script) => CliWrap.Cli.Wrap(script);
    public static Command GitCliCommand() => CliWrap.Cli.Wrap("git");
    public static Command NpmCliCommand() => CliWrap.Cli.Wrap("npm");
    public static Command DotnetCliCommand() => CliWrap.Cli.Wrap("dotnet");
}