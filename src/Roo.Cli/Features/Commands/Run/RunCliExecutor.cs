namespace Roo.Cli.Features.Commands.Run;

using Roo.Cli.Features.Commands.Core;
using global::System.Text;

public class RunCliExecutor<THandler>(IRooLogger logger) : ICliCommandExecutor<THandler>
{
    private readonly IRooLogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<Result<IRooCommandResponse, Error, Skipped>> RunCommandAsync(RooCommandRequest request)
    {
        // The first argument in request.Arguments is the full command string from the user (e.g. "ng serve")
        if (request.Arguments.Count == 0)
        {
            return Error.Validation("No command provided to run.");
        }

        var fullCommand = request.Arguments[0];
        var parts = fullCommand.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 0)
        {
             return Error.Validation("Empty command provided.");
        }

        var target = parts[0];
        var args = parts.Length > 1 ? parts[1] : string.Empty;

        var stdOutBuffer = new StringBuilder();
        var stdErrBuffer = new StringBuilder();

        try
        {
            var command = CliWrap.Cli.Wrap(target)
                .WithArguments(args)
                .WithWorkingDirectory(request.Repository.GetLocalRepoPath())
                .WithStandardOutputPipe(PipeTarget.Merge(
                    PipeTarget.ToStringBuilder(stdOutBuffer),
                    PipeTarget.ToDelegate(s => _logger.Log(s))))
                .WithStandardErrorPipe(PipeTarget.Merge(
                    PipeTarget.ToStringBuilder(stdErrBuffer),
                    PipeTarget.ToDelegate(s => _logger.Log(s))));

            var result = await command.ExecuteAsync();
            
            return RooCommandResponse.Create(
                request.Repository, 
                stdOutBuffer.ToString(), 
                stdErrBuffer.ToString());

        }
        catch (Exception ex)
        {
            return Error.Failure($"Command '{fullCommand}' failed: {ex.Message}");
        }
    }
}
