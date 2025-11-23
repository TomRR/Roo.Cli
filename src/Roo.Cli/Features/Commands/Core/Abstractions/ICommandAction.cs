namespace Roo.Cli.Features.Commands.Core;

public interface ICommandAction<TCommand> where TCommand : ICommand
{
    Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request);
}

