namespace Roo.Cli.Features.Commands.Git.Fetch;

public sealed partial class FetchCommandHandler(ICliCommandExecutor<FetchCommandHandler> executor) : ICommandHandler<FetchRequest>
{
    private readonly ICliCommandExecutor<FetchCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(FetchRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(FetchRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName);
        
        var args = builder.Build();
        return args;
    }
}