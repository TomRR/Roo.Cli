namespace Roo.Cli.Common.Models;

public record RooConfig
{
    public string? Title { get; init; }
    public List<Repository> Repositories { get; init; } = new();
}

public record Repository
{
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string Path { get; init; } = string.Empty;
}
