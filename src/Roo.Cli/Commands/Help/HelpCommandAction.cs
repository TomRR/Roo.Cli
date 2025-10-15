namespace Roo.Cli.Commands.Help;

public class HelpCommandAction : ICommandAction<HelpCommand>
{
    public async Task<Result<BufferedCommandResult, Error>> RunCommandAsync(Repository repository)
    {
        var result = await CliWrap.Cli.Wrap("git")
            .WithArguments(args => args
                .Add("clone")
                .Add($"{repository.Url}")
            )
            .WithStandardOutputPipe(PipeTarget.ToDelegate(ConsoleLogHelper.GetConsoleLog))
            .ExecuteBufferedAsync();

        return result;

    } 
}