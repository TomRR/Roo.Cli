var builder = CliAppBuilder.CreateDefaultBuilder(args);
builder
    .AddToolkitDependencies()
    .AddCommands();

builder.Services.AddRooDependencies();


var app = builder.Build();
await app.RunAsync(args);