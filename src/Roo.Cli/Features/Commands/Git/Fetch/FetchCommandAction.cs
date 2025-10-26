namespace Roo.Cli.Features.Commands.Git.Fetch;

public class FetchCommandAction : ICommandAction<FetchCommand>
{
    public async Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request)
    {
        ResetOutput();
        try
        {
            var result = await CliWrap.Cli.Wrap("git")
                .WithArguments(args => args
                    .Add("fetch")
                )
                .WithWorkingDirectory(request.Repository.GetLocalRepoPath())
                .WithStandardOutputPipe(PipeTarget.ToDelegate(GetConsoleLog))
                .ExecuteBufferedAsync();
            return RooCommandResponse.Create(request.Repository, result.StandardOutput);

        }
        catch (Exception e)
        {
            return Error.Conflict(e.Message);
        }
    }

    private string _output = string.Empty;
    private void ResetOutput() => _output = string.Empty;
    private void GetConsoleLog(string output)
    {
        _output = _output +  output + "\n";
    }
}