namespace Roo.Cli.Features.Commands.Git.Push;


public sealed partial class PushCommandHandler(ICliCommandExecutor<PushCommandHandler> executor) : ICommandHandler<PushRequest>
{
    private readonly ICliCommandExecutor<PushCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(PushRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(PushRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName);
            // .AddFlag("--interactive", request.Interactive)
            // .AddFlag("--force", request.Force);

        // if (request.SetUpstream)
        // {
        //     builder.AddFlag("--set-upstream");
        //
        //     if (!string.IsNullOrWhiteSpace(request.UpstreamRemote))
        //         builder.AddPositional(request.UpstreamRemote);
        //
        //     if (!string.IsNullOrWhiteSpace(request.UpstreamBranch))
        //         builder.AddPositional(request.UpstreamBranch);
        // }

        var args = builder.Build();
        return args;
    }
    
    // private IReadOnlyList<string> BuildCommand()
    // {
    //     var builder = CliCommandBuilder.Create()
    //         .Add(CommandName)
    //         .AddFlag("--interactive", Interactive)
    //         .AddFlag("--force", Force);
    //
    //     if (SetUpstream)
    //     {
    //         builder.AddFlag("--set-upstream");
    //
    //         if (!string.IsNullOrWhiteSpace(UpstreamRemote))
    //             builder.AddPositional(UpstreamRemote);
    //
    //         if (!string.IsNullOrWhiteSpace(UpstreamBranch))
    //             builder.AddPositional(UpstreamBranch);
    //     }
    //
    //     return builder.Build();
    // }
}