namespace TomRR.Cli.Tooling;

public interface ICommand
{
    Task RunAsync(string[] args);
    Task RunAsync();
}