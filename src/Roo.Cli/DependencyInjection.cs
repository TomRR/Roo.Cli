using Roo.Cli.Commands.Help;

namespace Roo.Cli;

public static class DependencyInjection
{
    public static IServiceCollection AddRooDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IDirectoryService, DirectoryService>();
        
        services.AddSingleton<IRooLogger, RooLogger>();
        services.AddSingleton<IRooUserInput, RooUserInput>();
        services.AddSingleton<IRooConfigService, RooConfigService>();
        
        services.AddSingleton<IPromptHandler, PromptHandler>();
        services.AddSingleton<IGitStatusParser, GitStatusParser>();
        services.AddTransient<IRepositoryActionLogger, RepositoryActionLogger>();
        services.AddTransient<IGitRepoStatusRenderer, GitRepoStatusRenderer>();
        
        services.AddSingleton<ICommandAction<StatusCommand>, StatusCommandAction>();        
        services.AddSingleton<ICommandAction<HelpCommand>, HelpCommandAction>();        
        services.AddSingleton<ICommandAction<CloneCommand>, CloneCommandAction>();        
        return services;
    }
    public static CliAppBuilder AddRooCommands(this CliAppBuilder builder)
    {
        builder.AddCommand<VersionCommand>("--version", "-v");
        builder.AddCommand<HelpCommand>("--help", "help", "-h");
        
        builder.AddCommand<InitCommand>("init");
        builder.AddCommand<CloneCommand>("clone");
        builder.AddCommand<StatusCommand>("status", "-s");
        
        return builder;
    }
}