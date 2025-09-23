using Roo.Cli.Cli.Nuget.Commands;

namespace Roo.Cli.Cli.Nuget;

public static class CliAppBuilderExtensions
{
    public static CliAppBuilder AddCommand<TCommand>(
        this CliAppBuilder builder, string name, string? shortName = null)
        where TCommand : class, ICommand
    {
        builder.AddDependencies(services =>
        {
            services.AddTransient<TCommand>();
            services.AddTransient<ICommand, TCommand>();
            

            services.AddSingleton<CommandDispatcher>(); // idempotent
        });

        // Post-build action to register with dispatcher
        builder.AddPostBuildAction(host =>
        {
            var dispatcher = host.Services.GetRequiredService<CommandDispatcher>();
            dispatcher.Register<TCommand>(name, shortName);
        });

        return builder;
    }
}
