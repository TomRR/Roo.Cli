namespace Roo.Cli.Features.Commands.Core;

public interface ICliCommandExecutor<THandler>
{
    Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request);
}