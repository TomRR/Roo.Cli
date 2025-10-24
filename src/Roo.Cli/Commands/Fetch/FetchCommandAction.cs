namespace Roo.Cli.Commands.Fetch;

public class FetchCommandAction : ICommandAction<FetchCommand>
{
    public async Task<Result<RepositoryActionResponse, Error, Skipped>> RunCommandAsync(RepositoryDto repository, bool force = false)
    {
        ResetOutput();
        try
        {
            var result = await CliWrap.Cli.Wrap("git")
                .WithArguments(args => args
                    .Add("fetch")
                )
                .WithWorkingDirectory(repository.GetLocalRepoPath())
                .WithStandardOutputPipe(PipeTarget.ToDelegate(GetConsoleLog))
                .ExecuteBufferedAsync();
            return RepositoryActionResponse.Create(repository, result.StandardOutput);

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