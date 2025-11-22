namespace Roo.Cli.Features.Commands.Git.Clone;

public sealed partial class CloneCommandHandler(ICliCommandExecutor<CloneCommandHandler> executor) : ICommandHandler<CloneRequest>
{
    private readonly ICliCommandExecutor<CloneCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(CloneRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(CloneRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName);
        
        var args = builder.Build();
        return args;
    }
}