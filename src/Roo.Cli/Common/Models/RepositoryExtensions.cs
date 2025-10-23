namespace Roo.Cli.Common.Models;

public static class RepositoryExtensions
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
}
