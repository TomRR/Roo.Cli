namespace Roo.Cli.Commands.Version;

public static class VersionCommand
{
    public static int Run(CliContext ctx)
    {
        Console.WriteLine($"Roo CLI version {VersionInfo.Version}");
        return 0;
    }
}