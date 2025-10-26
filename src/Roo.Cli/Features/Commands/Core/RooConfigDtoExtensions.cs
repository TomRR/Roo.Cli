namespace Roo.Cli.Features.Commands.Core;

public static class RooConfigDtoExtensions
{
    public static async Task ForEachRepositories(
        this IEnumerable<RepositoryDto> repositories,
        Func<RepositoryDto, Task> actionAsync)
    {
        foreach (var repo in repositories)
        {
            await actionAsync(repo);
        }
    }
    
    public static string GetLocalRepoPath(this RepositoryDto repository)
    {
        var basePath = string.IsNullOrWhiteSpace(repository.Path) ? "." : repository.Path;
        return global::System.IO.Path.Combine(basePath, repository.Name);
    }
}
