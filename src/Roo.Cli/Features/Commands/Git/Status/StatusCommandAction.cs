namespace Roo.Cli.Features.Commands.Git.Status;

public class StatusCommandAction : ICommandAction<StatusCommand>
{
    public async Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request)
    {
        try
        {
            var result = await CliWrapFactory.GitCliCommand()
                .WithArguments(request.Arguments)
                .WithWorkingDirectory(request.Repository.GetLocalRepoPath())
                .ExecuteBufferedAsync();
            
            var response = RooCommandResponse.Create(
                request.Repository, 
                result.StandardOutput, 
                result.StandardError);
            
            return response;

        }
        catch (Exception e)
        {
            return Error.Conflict(e.Message);
        }
    }
}