namespace Roo.Cli.Cli.Nuget.Commands;

[Command("--help", "-h", "-?")]
public class HelpCommand  : ICommand
{
    public Task RunAsync()
    {
        Console.WriteLine("Help");
        return Task.CompletedTask;
    }
}