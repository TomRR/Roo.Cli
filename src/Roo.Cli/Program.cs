var builder = CliAppBuilder.CreateDefaultBuilder(args);
builder.Services.AddRooDependencies();
builder.AddRooCommands();

var app = builder.Build();
await app.RunAsync(args);