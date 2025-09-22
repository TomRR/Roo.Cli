namespace Roo.Cli.Features.Commands.Git.Commit;

public sealed partial class CommitCommandHandler(ICliCommandExecutor<CommitCommandHandler> executor) : ICommandHandler<CommitRequest>
{
    private readonly ICliCommandExecutor<CommitCommandHandler> _executor = executor ?? throw new ArgumentNullException(nameof(executor));

    public Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(CommitRequest request)
    {
        var args = BuildCommand(request);
        var commandRequest = RooCommandRequest.Create(request.Repository, args);
        
        return _executor.RunCommandAsync(commandRequest);
    }
    
    private IReadOnlyList<string> BuildCommand(CommitRequest request)
    {
        var builder = CliCommandBuilder.Create()
            .Add(request.CommandName);
        
        var args = builder.Build();
        return args;
    }
    
    // private IReadOnlyList<string> BuildCommand()
    // {
    //     var builder = CliCommandBuilder.Create()
    //         .Add(CommandName)
    //         .AddFlag("--amend", Amend);
    //
    //     if (Message)
    //     {
    //         builder.AddFlag("--message");
    //
    //         if (!string.IsNullOrWhiteSpace(MessageArg))
    //         {
    //             builder.AddPositional(MessageArg);
    //         }
    //     }
    //     
    //     return builder.Build();
    // }
}