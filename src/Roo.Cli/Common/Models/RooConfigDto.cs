namespace Roo.Cli.Common.Models;

public record RooConfigDto(string? Title, OwnerDto? Owner, List<RepositoryDto> Repositories);

public record OwnerDto(string? Name);

public record RepositoryDto(string Name, string Url, string Path, string? Tags, string? Description);

public static class RepositoryDtoExtensions
{
    public static string GetLocalRepoPath(this RepositoryDto repository)
    {
        var basePath = string.IsNullOrWhiteSpace(repository.Path) ? "." : repository.Path;
        return System.IO.Path.Combine(basePath, repository.Name);
    }

}