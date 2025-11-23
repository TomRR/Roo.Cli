namespace Roo.Cli.Features.Commands.Run;

using Roo.Cli.Features.Commands.Core;

public sealed partial class RunCommandHandler(ICliCommandExecutor<RunCommandHandler> executor) : ICommandHandler<RunRequest>
{
    private readonly ICliCommandExecutor<RunCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(RunRequest request)
    {
        // Pass the command string as a single argument to the executor
        var commandRequest = RooCommandRequest.Create(request.Repository, [request.CommandToRun]);
        
        return _executor.RunCommandAsync(commandRequest);
    }
}
