namespace TomRR.Cli.Tooling;

public static class CliAppBuilderExtensions
{
    public static CliAppBuilder AddCommand<TCommand>(
        this CliAppBuilder builder, string name, params string[] shortNames)
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
            dispatcher.Register<TCommand>(name, shortNames);
        });

        return builder;
    }
}
