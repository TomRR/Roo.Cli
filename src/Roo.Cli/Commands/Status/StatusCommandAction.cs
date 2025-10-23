namespace Roo.Cli.Commands.Status;

public class StatusCommandAction : ICommandAction<StatusCommand>
{
    public async Task<Result<RepositoryActionResponse, Error, Skipped>> RunCommandAsync(RepositoryDto repository, bool force = false)
    {
        ResetOutput();
        var result = await CliWrap.Cli.Wrap("git")
            .WithArguments(args => args
                .Add("status")
                .Add("--porcelain=v2")
                .Add("--branch")
            )
            .WithWorkingDirectory(repository.GetLocalRepoPath())
            .WithStandardOutputPipe(PipeTarget.ToDelegate(GetConsoleLog))
            .ExecuteBufferedAsync();

        return RepositoryActionResponse.Create(repository, result.StandardOutput);
    }

    private string _output = string.Empty;
    private void ResetOutput() => _output = string.Empty;
    private void GetConsoleLog(string output)
    {
        _output = _output +  output + "\n";
    }
}