namespace Roo.Cli.Commands.Status;

public class StatusCommandAction : ICommandAction<StatusCommand>
{
    public async Task<Result<BufferedCommandResult, Error>> RunCommandAsync(Repository repository)
    {
        var result = await CliWrap.Cli.Wrap("git")
            .WithArguments(args => args
                .Add("-C")
                .Add($"{repository.Path}")
                .Add("status")
            )
            .WithStandardOutputPipe(PipeTarget.ToDelegate(ConsoleLogHelper.GetConsoleLog))
            .ExecuteBufferedAsync();

        return result;
    }
}