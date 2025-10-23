namespace Roo.Cli.Commands;

public record RepositoryActionResponse
{
    public RepositoryDto Repository { get; }
    public string StandardOutput { get; }
    private RepositoryActionResponse(RepositoryDto repository, string standardOutput)
    {
        Repository = repository;
        StandardOutput = standardOutput;
    }

    public static RepositoryActionResponse Create(RepositoryDto repository, string stdout = "") 
        => new(repository, stdout);
}