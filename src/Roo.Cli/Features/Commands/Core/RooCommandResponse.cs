namespace Roo.Cli.Features.Commands.Core;

public interface IRooCommandResponse
{
    public RepositoryDto Repository { get; }
    public string StandardOutput { get; }
    public string StandardError { get; }
    
    public bool HasErrors => !string.IsNullOrWhiteSpace(StandardError);

}
public sealed record RooCommandResponse : IRooCommandResponse
{
    public RepositoryDto Repository { get; }
    public string StandardOutput { get; }
    public string StandardError { get; }

    private RooCommandResponse(
        RepositoryDto repository, 
        string standardOutput, 
        string standardError)
    {
        Repository = repository;
        StandardOutput = standardOutput;
        StandardError = standardError;
    }

    public static RooCommandResponse Create(
        RepositoryDto repository, 
        string stdout = "", 
        string stderr = "")
        => new(repository, stdout ?? string.Empty, stderr ?? string.Empty);

    public bool HasErrors => !string.IsNullOrWhiteSpace(StandardError);
}
