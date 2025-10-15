using Roo.Cli.Commands.Help;

namespace Roo.Cli;

public static class DependencyInjection
{
    public static IServiceCollection AddRooDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IDirectoryService, DirectoryService>();
        services.AddSingleton<IRooLogger, RooLogger>();
        services.AddSingleton<IRooConfigRetriever, RooConfigRetriever>();
        
        services.AddSingleton<ICommandAction<StatusCommand>, StatusCommandAction>();        
        return services;
    }
    public static CliAppBuilder AddRooCommands(this CliAppBuilder builder)
    {
        builder.AddCommand<VersionCommand>("--version", "-v");
        builder.AddCommand<HelpCommand>("--help", "-h", "-?");
        
        builder.AddCommand<InitCommand>("init");
        builder.AddCommand<StatusCommand>("status", "-s");
        builder.AddCommand<CloneCommand>("clone");
        
        return builder;
    }
}