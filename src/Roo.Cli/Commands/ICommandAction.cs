namespace Roo.Cli.Commands;

public interface ICommandAction<TCommand> where TCommand : ICommand
{
    Task<Result<BufferedCommandResult, Error>> RunCommandAsync(Repository repository);
}
