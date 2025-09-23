// var builder = CoconaApp.CreateBuilder();
// builder.Services.AddApplicationDependencies();
//
// var app = builder.Build();
// app.AddRooCommands();
// app.Run();
//
// using System.Diagnostics.CodeAnalysis;
// using Cocona;
//
// Console.WriteLine("staarting");
// var app = CoconaApp.Create();
//
// // Register your command class explicitly
// app.AddCommands<InitCommand>();
//
// app.Run();
//
// // Dummy method to preserve commands for AOT trimming
// [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(InitCommand))]
// static void PreserveCommands() { }
//
// public class InitCommand
// {
//     [Command("init", Description = "Initialize a Roo project")]
//     public void Init(string path = ".")
//     {
//         Console.WriteLine($"Initializing config in {path}");
//     }
//
//     [Command("i")]
//     public void ShortInit(string path = ".")
//     {
//         Console.WriteLine($"(Alias) Initializing config in {path}");
//     }
// }

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Roo.Cli;
using Roo.Cli.Cli.Nuget;
using Roo.Cli.Cli.Nuget.Commands;
using Roo.Cli.Commands.Status;

// var builder = CliAppBuilder.Create(args);
//
// builder.Services.AddDependencies();
//
// // builder.AddCommands<InitCommand>();
//
// builder.Build();
// app.Run();

// var worker = host.Services.GetRequiredService<IWorker>();
// await worker.RunAsync();
//
// await host.StopAsync();

var builder = CliAppBuilder.Create(args);
builder.Services.AddCooDependencies();

builder.AddCommand<VersionCommand>("--version", "-v");
builder.AddCommand<HelpCommand>("--help", "-h");
builder.AddCommand<InitCommand>("init");
builder.AddCommand<StatusCommand>("status", "-s");

var app = builder.Build();
await app.RunAsync(args);







// var ctx = CliParser.Parse(args);
//
// var commands = new Dictionary<string, Func<CliContext, int>>(StringComparer.OrdinalIgnoreCase)
// {
//     ["init"] = InitCommand.Run,
//     ["--version"] = VersionCommand.Run,
//     ["-v"] = VersionCommand.Run
// };
//
// if (commands.TryGetValue(ctx.Command, out var handler))
//     return handler(ctx);
//
// Console.WriteLine($"Unknown command: {ctx.Command}");
// return 1;