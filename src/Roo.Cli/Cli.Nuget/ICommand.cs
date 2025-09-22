namespace Roo.Cli.Cli.Nuget;

public interface ICommand
{
    Task RunAsync(string[] args);
}