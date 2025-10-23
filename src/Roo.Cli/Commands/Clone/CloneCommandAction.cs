namespace Roo.Cli.Commands.Clone;

public class CloneCommandAction : ICommandAction<CloneCommand>
{
    public async Task<Result<RepositoryActionResponse, Error, Skipped>> RunCommandAsync(RepositoryDto repository, bool force = false)
    {

        var command = CreateCommand(repository);
        if (command is null)
        {
            return Error.Conflict("Command cannot be null");
        }
        
        var result = await command
            .WithStandardOutputPipe(PipeTarget.ToDelegate(InternalLogOutput.GetConsoleLog))
            .ExecuteBufferedAsync();
        

        return result.IsSuccess 
            ? RepositoryActionResponse.Create(repository, result.StandardOutput)
            : Error.Conflict(result.StandardError);
    }

    private static Command? CreateCommand(RepositoryDto repository)
    {
        var withPath = IsPathSet(repository);

        var command = CliWrap.Cli.Wrap("git");
        return withPath
            ? CommandWithPath(command, repository)
            : CommandWithoutPath(command, repository);
    }
    private static Command BuildGitCloneCommand(RepositoryDto repository)
    {
        var args = new List<string> { "clone" };
        
        args.Add(repository.Url);

        if (IsPathSet(repository))
        {
            args.Add(repository.Path);
        }
        return CliWrap.Cli.Wrap("git").WithArguments(args);
    }

    private static Command CommandWithPath(Command command,RepositoryDto repository) 
        => command.WithArguments(args => args
            .Add("clone")
            .Add($"{repository.Url}")
            .Add($"{repository.GetLocalRepoPath()}"));
    private static Command CommandWithoutPath(Command command,RepositoryDto repository)
        => command.WithArguments(args => args
            .Add("clone")
            .Add($"{repository.Url}"));
    
    private static bool IsPathSet(RepositoryDto repository) =>
        !string.IsNullOrWhiteSpace(repository.Path) && repository.Path != "./";
}