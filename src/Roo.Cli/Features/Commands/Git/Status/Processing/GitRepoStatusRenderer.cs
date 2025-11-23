namespace Roo.Cli.Features.Commands.Git.Status.Processing;

public interface IGitRepoStatusRenderer
{
    public void Render(RepositoryDto repo, GitRepoStatusInfo parsed, bool onlyIfChanges = false);
}

public class GitRepoStatusRenderer(IRooLogger logger) : IGitRepoStatusRenderer
{
    public void Render(RepositoryDto repo, GitRepoStatusInfo parsed, bool onlyIfChanges = false)
    {
        var text = GitRepoStatusFormatter.Format(repo, parsed, onlyIfChanges);
        if (!string.IsNullOrEmpty(text))
        {
            logger.Log(text);
        }    
    }
}
