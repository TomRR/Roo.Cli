namespace Roo.Cli.UI.Components;


public static partial class Components
{
    public static class Messages
    {
        public static string RepoSkipped(string repositoryName)
            => $"{Icons.FastForwardIcon}  [yellow]{repositoryName} Skipped[/]";
        public static string GetRepoName(string repositoryName)
            => $"{Icons.GearIcon} Repository: {repositoryName}";
        public static string GetCloningWithRepoName(string repositoryUrl)
            => $"{Icons.GearIcon} Cloning {repositoryUrl}...";
        public static string GetPullingWithRepoName(string repositoryName)
            => $"{Icons.GearIcon} Pulling {repositoryName}...";
        public static string GetCommittingWithRepoName(string repositoryName)
            => $"{Icons.GearIcon} Committing {repositoryName}...";
        public static string GetPushingWithRepoName(string repositoryName)
            => $"{Icons.GearIcon} Pushing {repositoryName}...";
        public static string GetAddingWithRepoName(string repositoryName)
            => $"{Icons.GearIcon} Adding {repositoryName}...";
        public static string GetFetchingWithRepoName(string repositoryName)
            => $"{Icons.GearIcon} Fetching {repositoryName}...";
    
        public static string DeletingExistingPath(string path)
            => $"{Icons.BroomIcon} Deleting existing directory: {path}";
    
        public static string DoneWithCheck()
            => $"{Icons.CheckIcon}  DONE";
    
        public static string InvalidRepoUrl(string repositoryName)
            => $"[red] {repositoryName}: Url is missing[/]";
        public static string NoGitRepository(string repositoryPath)
            => $"{Icons.WarningIcon}  [red]{repositoryPath} is not a git repository[/]";
    }    
}
