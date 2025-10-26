namespace Roo.Cli.Features.Commands.Git.Add;

public class AddCommandHandler(ICliCommandExecutor<AddCommandHandler> executor) : ICommandHandler<AddRequest>
{
    private readonly ICliCommandExecutor<AddCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(AddRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(AddRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName)
            .AddFlag("--force", request.Force);
        
        var args = builder.Build();
        return args;
    }
}