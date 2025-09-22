    using Roo.Cli.Features.Commands.Git.Status.Processing;

    namespace Roo.Cli;

    public static class DependencyInjection
    {
        public static IServiceCollection AddRooDependencies(this IServiceCollection services)
        {
            services
                .AddCommandCoreDependencies()
                .AddGitCommandDependencies()
                .AddSystemCommandDependencies()
                .AddInfrastructureDependencies();
        
            return services;
        }
        private static IServiceCollection AddCommandCoreDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IRooCommandValidator, RooCommandValidator>();
            services.AddSingleton<RooCommandContext>();
            
            return services;
        }

        private static IServiceCollection AddGitCommandDependencies(this IServiceCollection services)
        {
            // Add
            services.AddSingleton<ICliCommandExecutor<AddCommandHandler>, GitCliExecutor<AddCommandHandler>>();
            services.AddTransient<ICommandHandler<AddRequest>, AddCommandHandler>();
            
            // Clone
            // services.AddSingleton<ICliCommandExecutor<CloneCommandHandler>, GitCliExecutor<CloneCommandHandler>>();
            // services.AddTransient<ICommandHandler<CloneRequest>, CloneCommandHandler>();
            services.AddSingleton<ICommandAction<CloneCommand>, CloneCommandAction>();        

            // Commit
            services.AddSingleton<ICliCommandExecutor<CommitCommandHandler>, GitCliExecutor<CommitCommandHandler>>();
            services.AddTransient<ICommandHandler<CommitRequest>, CommitCommandHandler>();
            
            // Fetch
            services.AddSingleton<ICliCommandExecutor<FetchCommandHandler>, GitCliExecutor<FetchCommandHandler>>();
            services.AddTransient<ICommandHandler<FetchRequest>, FetchCommandHandler>();
            
            // Pull
            services.AddSingleton<ICliCommandExecutor<PullCommandHandler>, GitCliExecutor<PullCommandHandler>>();
            services.AddTransient<ICommandHandler<PullRequest>, PullCommandHandler>();
            
            // Push
            services.AddSingleton<ICliCommandExecutor<PushCommandHandler>, GitCliExecutor<PushCommandHandler>>();
            services.AddTransient<ICommandHandler<PushRequest>, PushCommandHandler>();
            
            // Status
            services.AddSingleton<ICliCommandExecutor<StatusCommandHandler>, GitCliExecutor<StatusCommandHandler>>();
            services.AddTransient<ICommandHandler<StatusRequest>, StatusCommandHandler>();
            services.AddSingleton<IGitStatusParser, GitStatusParser>();
            services.AddTransient<IGitRepoStatusRenderer, GitRepoStatusRenderer>();
            
            return services;
        }
        private static IServiceCollection AddSystemCommandDependencies(this IServiceCollection services)
        {
            // Help
            services.AddSingleton<ICommandAction<HelpCommand>, HelpCommandAction>();  
            // Init
            // Version
            
            return services;
        }
        
        private static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Config
            services.AddSingleton<IRooConfigService, RooConfigService>();
            
            // IO
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IFileService, FileService>();
            
            // Logging
            services.AddSingleton<IRooLogger, RooLogger>();

            // Prompting
            services.AddSingleton<IPromptWrapper, PromptWrapper>();
            services.AddSingleton<IRooPrompt, RooPrompt>();
            
            return services;
        }
    }