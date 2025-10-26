using System.Text.RegularExpressions;

namespace Roo.Cli.Features.Commands.Git.Status;

public interface IGitStatusParser
{
    public GitRepoStatusInfo Parse(string output);
}
public class GitStatusParser : IGitStatusParser
{
    public GitRepoStatusInfo Parse(string output)
    {
        var info = new GitRepoStatusInfo();

        if (string.IsNullOrWhiteSpace(output))
            return info;

        var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            switch (line)
            {
                case var l when l.StartsWith("# branch.head"):
                    info.BranchHeadName = l.Replace("# branch.head", "").Trim();
                    break;

                case var l when l.StartsWith("# branch.upstream"):
                    info.BranchUpstreamName = l.Replace("# branch.upstream", "").Trim();
                    break;

                case var l when l.StartsWith("# branch.ab"):
                    var match = Regex.Match(l, @"# branch\.ab \+(\d+) -(\d+)");
                    if (match.Success)
                    {
                        info.Ahead = int.Parse(match.Groups[1].Value);
                        info.Behind = int.Parse(match.Groups[2].Value);
                    }
                    break;

                case var l when l.StartsWith("? "):
                    info.UntrackedFiles.Add(l[2..].Trim());
                    break;

                case var l when l.StartsWith("! "):
                    info.IgnoredFiles.Add(l[2..].Trim());
                    break;

                case var l when Regex.IsMatch(l, @"^[12] "):
                    ParseChangedFileLine(l, info);
                    break;

                default:
                    // Anything else unexpected
                    break;
            }
        }

        // Determine overall repo status
        info.Status = DetermineRepoStatus(info);
        return info;
    }

    private static void ParseChangedFileLine(string line, GitRepoStatusInfo info)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2) return;

        var xy = parts[1];
        var file = parts.Last();

        char x = xy.Length > 0 ? xy[0] : '.';
        char y = xy.Length > 1 ? xy[1] : '.';

        // X = index (staged)
        switch (x)
        {
            case 'A':
            case 'M':
            case 'D':
            case 'R':
            case 'C':
                info.StagedFiles.Add(file);
                break;
            case 'U':
                info.ConflictedFiles.Add(file);
                break;
        }

        // Y = worktree (unstaged)
        switch (y)
        {
            case 'M':
                info.ModifiedFiles.Add(file);
                break;
            case 'D':
                info.DeletedFiles.Add(file);
                break;
            case 'U':
                info.ConflictedFiles.Add(file);
                break;
        }
    }

    private static RepoStatus DetermineRepoStatus(GitRepoStatusInfo info)
    {
        if (info.ConflictedFiles.Count > 0)
        {
            return RepoStatus.Error;
        }        
        if (info.UntrackedFiles.Count > 0)
        {
            return RepoStatus.Untracked;
        }        
        if (info.StagedFiles.Count > 0)
        {
            return RepoStatus.Staged;
        }        
        if (info.ModifiedFiles.Count > 0 || info.DeletedFiles.Count > 0)
        {
            return RepoStatus.Modified;
        }        
        if (info.Ahead > 0 && info.Behind > 0)
        {
            return RepoStatus.Diverged;
        }        
        if (info.Ahead > 0)
        {
            return RepoStatus.Ahead;
        }        
        if (info.Behind > 0)
        {
            return RepoStatus.Behind;
        }
        return RepoStatus.Clean;
    }
}