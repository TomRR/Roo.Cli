using Microsoft.Extensions.Logging;
using Roo.Cli.Cli.Nuget;
using Spectre.Console;

namespace Roo.Cli.Commands.Init;
//
// [Command("init")]
// public class InitCommand : ICommand
// {
//     
//     private readonly ILogger<InitCommand> _logger;
//
//     public InitCommand(ILogger<InitCommand> logger)
//     {
//         _logger = logger;
//     }
//
//     public Task RunAsync(string[] args)
//     {
//         _logger.LogInformation("Repository initialized.");
//         return Task.CompletedTask;
//     }
//     
//     public static int Run(CliContext ctx)
//     {
//         var path = ctx.Arguments.Count > 0 ? ctx.Arguments[0] : Directory.GetCurrentDirectory();
//         var filePath = Path.Combine(path, "roo.toml");
//
//         if (File.Exists(filePath))
//         {
//             Console.WriteLine("roo.toml already exists!");
//             return 1;
//         }
//
//         File.WriteAllText(filePath, "# Roo CLI configuration\n");
//         Console.WriteLine($"Created {filePath}");
//         return 0;
//     }
// }
// public class InitCommand : ICommand
// {
//     private readonly ILogger<InitCommand> _logger;
//
//     public InitCommand(ILogger<InitCommand> logger)
//     {
//         _logger = logger;
//     }
//
//     public Task RunAsync(string[] args)
//     {
//         Console.WriteLine("DONE");
//         // _logger.LogInformation("Repository initialized.");
//         return Task.CompletedTask;
//     }
//
//     public Task RunAsync()
//     {
//         throw new NotImplementedException();
//     }
// }

[Command("init")]
public class InitCommand : ICommand
{
    private readonly ILogger<InitCommand> _logger;

    [Option("--path", "-p")]
    public string Path { get; set; } = ".";

    [Option("--force", "-f", hasValue: false)]
    public bool Force { get; set; }

    [Argument(0)]
    public string? Template { get; set; }

    public InitCommand(ILogger<InitCommand> logger)
    {
        _logger = logger;
    }
    public Task RunAsync(string[] args)
    {
        throw new NotImplementedException();
    }

    public Task RunAsync()
    {
        Console.WriteLine($"Init with Path={Path}, Force={Force}, Template={Template}");
        _logger.LogInformation("Repository initialized.");
        AnsiConsole.Write(new Markup("[bold yellow]Hello[/] [red]World![/]"));


        return Task.CompletedTask;
    }
}