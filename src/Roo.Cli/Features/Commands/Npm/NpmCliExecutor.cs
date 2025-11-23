namespace Roo.Cli.Features.Commands.Npm;

public class NpmCliExecutor<THandler> : ICliCommandExecutor<THandler>
{
    public async Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request)
    {
        try
        {
            var result = await CliWrapFactory.NpmCliCommand()
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
