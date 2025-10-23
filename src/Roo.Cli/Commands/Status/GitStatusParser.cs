using System.Text.RegularExpressions;

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
            if (line.StartsWith("# branch.head"))
            {
                info.BranchHeadName = line.Replace("# branch.head", "").Trim();
            }
            if (line.StartsWith("# branch.upstream"))
            {
                info.BranchUpstreamName = line.Replace("# branch.upstream", "").Trim();
            }
            else if (line.StartsWith("# branch.ab"))
            {
                var match = Regex.Match(line, @"# branch\.ab \+(\d+) -(\d+)");
                if (match.Success)
                {
                    info.Ahead = int.Parse(match.Groups[1].Value);
                    info.Behind = int.Parse(match.Groups[2].Value);
                }
            }
            else if (line.StartsWith("? "))
            {
                info.UntrackedFiles.Add(line[2..].Trim());
            }
            else if (Regex.IsMatch(line, @"^[12] "))
            {
                // porcelain v2 format for changed files starts with '1' or '2'
                // Example: "1 .M N... ..."
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    var flags = parts[1];
                    if (flags.Contains('M') || flags.Contains('A') || flags.Contains('D'))
                    {
                        var file = parts.Last();
                        info.ModifiedFiles.Add(file);
                    }
                }
            }
        }

        // Determine status
        if (info.ModifiedFiles.Count > 0)
            info.Status = RepoStatus.Modified;
        else if (info.UntrackedFiles.Count > 0)
            info.Status = RepoStatus.Untracked;
        else if (info.Ahead > 0 && info.Behind > 0)
            info.Status = RepoStatus.Diverged;
        else if (info.Ahead > 0)
            info.Status = RepoStatus.Ahead;
        else if (info.Behind > 0)
            info.Status = RepoStatus.Behind;
        else
            info.Status = RepoStatus.Clean;

        return info;
    }
}