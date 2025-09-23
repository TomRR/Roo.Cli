using Roo.Cli.Cli.Nuget.Attributes;

namespace Roo.Cli.Cli.Nuget.Commands;

[Command]
public class HelpCommand  : ICommand
{
    public Task RunAsync(string[] args)
    {
        Console.WriteLine("Help");
        return Task.CompletedTask;
    }
}