using Microsoft.Extensions.Logging;
using Roo.Cli.Cli.Nuget;

namespace Roo.Cli.Commands.Init;

[Command("init")]
public class InitCommand : ICommand
{
    
    private readonly ILogger<InitCommand> _logger;

    public InitCommand(ILogger<InitCommand> logger)
    {
        _logger = logger;
    }

    public Task RunAsync(string[] args)
    {
        _logger.LogInformation("Repository initialized.");
        return Task.CompletedTask;
    }
    
    public static int Run(CliContext ctx)
    {
        var path = ctx.Arguments.Count > 0 ? ctx.Arguments[0] : Directory.GetCurrentDirectory();
        var filePath = Path.Combine(path, "roo.toml");

        if (File.Exists(filePath))
        {
            Console.WriteLine("roo.toml already exists!");
            return 1;
        }

        File.WriteAllText(filePath, "# Roo CLI configuration\n");
        Console.WriteLine($"Created {filePath}");
        return 0;
    }
}