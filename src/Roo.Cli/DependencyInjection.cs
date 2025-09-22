using Roo.Cli.Cli.Nuget;

namespace Roo.Cli;

public static class DependencyInjection
{
    public static IServiceCollection AddCooCommands(this IServiceCollection services)
    {
        services.AddTransient<ICommand, InitCommand>();
        // services.AddTransient<ICommand, StatusCommand>();
        // services.AddTransient<ICommand, PullCommand>();
        // services.AddTransient<ICommand, PushCommand>();
        
        return services;
    }
}