namespace Roo.Cli.Features.Commands.System.Help;

public class HelpCommandAction : ICommandAction<HelpCommand>
{
    public async Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request)
    {
        var result = await CliWrap.Cli.Wrap("git")
            .WithArguments(args => args
                .Add("help")
                .Add($"{request.Repository.Url}")
            )
            .WithStandardOutputPipe(PipeTarget.ToDelegate(GetConsoleLog))
            .ExecuteBufferedAsync();

        return RooCommandResponse.Create(request.Repository, result.StandardOutput);
    }
    private string _output = string.Empty;

    private void GetConsoleLog(string output)
    {
        _output = _output +  output + "\n";
    }
}