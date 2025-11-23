namespace Roo.Cli.Features.Commands.Npm.Run;

public sealed partial class RunCommandHandler(ICliCommandExecutor<RunCommandHandler> executor) : ICommandHandler<RunRequest>
{
    private readonly ICliCommandExecutor<RunCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(RunRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(RunRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName);
        
        var args = builder.Build();
        return args;
    }
}
