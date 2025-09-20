var builder = CoconaApp.CreateBuilder();
builder.Services.AddApplicationDependencies();

var app = builder.Build();
app.AddRooCommands();
app.Run();