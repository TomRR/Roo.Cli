namespace Roo.Cli.Features.Commands.Core;

public interface IRooCommandRequest
{
    public RepositoryDto Repository { get; }
    public IReadOnlyList<string> Arguments { get; }
}

public sealed record RooCommandRequest : IRooCommandRequest
{
    public RepositoryDto Repository { get; }
    public IReadOnlyList<string> Arguments { get; }
    private RooCommandRequest(RepositoryDto repository, IReadOnlyList<string> arguments)
    {
        Repository = repository;
        Arguments = arguments;
    }

    public static RooCommandRequest Create(RepositoryDto repository, IReadOnlyList<string> arguments) 
        => new(
            repository, 
            arguments
            );
}