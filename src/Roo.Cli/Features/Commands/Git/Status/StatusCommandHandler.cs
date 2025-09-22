namespace Roo.Cli.Features.Commands.Git.Status;


public sealed partial class StatusCommandHandler(ICliCommandExecutor<StatusCommandHandler> executor) : ICommandHandler<StatusRequest>
{
    private readonly ICliCommandExecutor<StatusCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(StatusRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(StatusRequest request) 
        => CliCommandBuilder.Create()
            .Add(request.CommandName)
            .Add("--porcelain=v2")
            .Add("--branch").Build();
}