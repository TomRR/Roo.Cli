namespace Roo.Cli.Features.Commands.Npm.Install;

public sealed partial class InstallCommandHandler(ICliCommandExecutor<InstallCommandHandler> executor) : ICommandHandler<InstallRequest>
{
    private readonly ICliCommandExecutor<InstallCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(InstallRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(InstallRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName);
        
        var args = builder.Build();
        return args;
    }
}