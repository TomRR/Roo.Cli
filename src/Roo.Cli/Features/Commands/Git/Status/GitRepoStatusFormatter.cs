using System.Text;

namespace Roo.Cli.Features.Commands.Git.Status
{
    public static class GitRepoStatusFormatter
    {
        public static string Format(RepositoryDto repository, GitRepoStatusInfo parsed, bool onlyIfChanges = false)
        {
            var sb = new StringBuilder();

            sb.AppendLine(Components.Messages.GetRepoName(repository.Name));
            sb.AppendLine($"{Icons.BranchIcon} [yellow]Branch:[/] Head: {parsed.BranchHeadName} {Icons.ArrowRightSimpleIcon} {parsed.BranchUpstreamName}");

            if (parsed.IsClean())
            {
                if (onlyIfChanges)
                    return string.Empty;

                sb.AppendLine($"[green]{Icons.CheckSimpleIcon} Repository is clean[/]");
                return sb.ToString();
            }

            AppendSection(sb, "[green]Staged files:[/]\nuse \"git restore --staged <file>...\" to unstage", parsed.StagedFiles);
            AppendSection(sb, "[orange1]Modified files:[/]", parsed.ModifiedFiles);
            AppendSection(sb, "[red]Deleted files:[/]", parsed.DeletedFiles);
            AppendSection(sb, "[yellow]Untracked files:[/]", parsed.UntrackedFiles);
            AppendSection(sb, "[grey]Ignored files:[/]", parsed.IgnoredFiles);
            AppendSection(sb, $"[red]{Icons.WarningIcon} Conflicted files:[/]", parsed.ConflictedFiles);

            if (parsed.Ahead > 0 || parsed.Behind > 0)
            {
                sb.AppendLine($"[blue]{Icons.ArrowUpSimpleIcon}{parsed.Ahead} ahead, {Icons.ArrowDownSimpleIcon}{parsed.Behind} behind[/]");
            }

            return sb.ToString();
        }

        private static void AppendSection(StringBuilder sb, string title, IReadOnlyCollection<string> files)
        {
            if (files == null || files.Count == 0)
                return;

            sb.AppendLine(title);
            foreach (var file in files)
                sb.AppendLine($"  [grey]- {file}[/]");
        }
    }
}