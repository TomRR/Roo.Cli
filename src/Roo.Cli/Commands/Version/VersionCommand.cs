namespace Roo.Cli.Cli.Nuget.Commands;

[Command]
public class VersionCommand :  ICommand
{
    public VersionCommand() { }
    public Task RunAsync(string[] args)
    {
        Console.WriteLine("Version");
        return Task.CompletedTask;

    }

    public Task RunAsync()
    {
        Console.WriteLine("Version");
        return Task.CompletedTask;
    }
}