namespace Roo.Cli.Common;

public static class RepositoryExtensions
{
    public static async Task ForEachRepositories(
        this IEnumerable<Repository> repositories,
        Func<Repository, Task> actionAsync,
            IRooLogger logger)
    {
        foreach (var repo in repositories)
        {
            if (Directory.Exists(Path.Combine(repo.Path, ".git")))
            {
                await actionAsync(repo);
                continue;
            }            

            logger.Log($"⚠️  {repo.Path} is not a git repository.");
        }
    }
}
