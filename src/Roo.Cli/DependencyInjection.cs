using Roo.Cli.Cli.Nuget;

namespace Roo.Cli;

public static class DependencyInjection
{
    public static IServiceCollection AddCooDependencies(this IServiceCollection services)
    {
        services.AddTransient<ITest, TestService>();
        // services.AddTransient<ICommand, StatusCommand>();
        // services.AddTransient<ICommand, PullCommand>();
        // services.AddTransient<ICommand, PushCommand>();
        
        return services;
    }
}