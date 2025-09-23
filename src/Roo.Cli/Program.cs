using Roo.Cli;
using Roo.Cli.Cli.Nuget.Commands;
using Roo.Cli.Commands.Status;

var builder = CliAppBuilder.CreateDefaultBuilder(args);
builder.Services.AddSerilogLogging();
builder.Services.AddRooDependencies();
builder.AddRooCommands();

var app = builder.Build();
await app.RunAsync(args);