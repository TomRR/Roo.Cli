using Roo.Cli.Cli.Nuget;
using Roo.Cli.Cli.Nuget.Commands;
using Roo.Cli.Commands.Status;

namespace Roo.Cli;

public static class DependencyInjection
{
    public static IServiceCollection AddRooDependencies(this IServiceCollection services)
    {
        services.AddTransient<IFileService, FileService>();
        
        return services;
    }
    public static CliAppBuilder AddRooCommands(this CliAppBuilder builder)
    {
        builder.AddCommand<VersionCommand>("--version", "-v");
        builder.AddCommand<HelpCommand>("--help", "-h");
        
        builder.AddCommand<InitCommand>("init");
        builder.AddCommand<StatusCommand>("status", "-s", "-stet");
        
        return builder;
    }
}