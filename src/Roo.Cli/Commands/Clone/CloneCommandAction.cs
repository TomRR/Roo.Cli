namespace Roo.Cli.Commands.Clone;

public class CloneCommandAction : ICommandAction<CloneCommand>
{
    public async Task<Result<BufferedCommandResult, Error>> RunCommandAsync(Repository repository)
    {
        if (string.IsNullOrWhiteSpace(repository.Url))
        {
            return Error.Conflict($"{repository.Name}: Url is missing");
        }
        
        var path = string.IsNullOrWhiteSpace(repository.Path) ? "." : repository.Path;
        var result = await CliWrap.Cli.Wrap("git")
            .WithArguments(args => args
                .Add("clone")
                .Add($"{repository.Url}")
                .Add($"{path}")
            )
            .WithStandardOutputPipe(PipeTarget.ToDelegate(ConsoleLogHelper.GetConsoleLog))
            .ExecuteBufferedAsync();

        return result;

    } 
}