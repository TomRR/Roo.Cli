namespace Roo.Cli.Features.Commands.Git.Pull;


public sealed partial class PullCommandHandler(ICliCommandExecutor<PullCommandHandler> executor) : ICommandHandler<PullRequest>
{
    private readonly ICliCommandExecutor<PullCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(PullRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(PullRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName)
            .AddFlag("--force", request.Force);
        
        var args = builder.Build();
        return args;
    }
}