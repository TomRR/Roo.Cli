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

if (args.Length == 0)
{
    Console.WriteLine("No arguments provided.");
}
else
{
    Console.WriteLine("Arguments:");
    foreach (var arg in args)
    {
        Console.WriteLine($"- {arg}");
    }
}