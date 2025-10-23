using ResultType;

namespace Roo.Cli.Commands.Help;

public class HelpCommandAction : ICommandAction<HelpCommand>
{
    public async Task<Result<RepositoryActionResponse, Error, Skipped>> RunCommandAsync(RepositoryDto repository, bool force = false)
    {
        var result = await CliWrap.Cli.Wrap("git")
            .WithArguments(args => args
                .Add("help")
                .Add($"{repository.Url}")
            )
            .WithStandardOutputPipe(PipeTarget.ToDelegate(GetConsoleLog))
            .ExecuteBufferedAsync();

        return RepositoryActionResponse.Create(repository, result.StandardOutput);
    }
    private string _output = string.Empty;

    private void GetConsoleLog(string output)
    {
        _output = _output +  output + "\n";
    }
}