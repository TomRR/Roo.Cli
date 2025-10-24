using Spectre.Console;

namespace Roo.Cli.Commands;

public static class LoggingComponents
{
    public static Table GetCloningResultPanel(List<CommandResultType> results)
    {
        var success = results.Count(r => r == CommandResultType.Success);
        var skipped = results.Count(r => r == CommandResultType.Skipped);
        var failed = results.Count(r => r == CommandResultType.Failed);
        
        var table = new Table().Border(TableBorder.None);
        table.AddColumn(new TableColumn(""));
        table.AddRow($"[green]{Icons.CheckIcon} Cloned:[/]   {success}");
        table.AddRow($"[yellow]{Icons.WarningIcon}Skipped:[/]  {skipped}");
        table.AddRow($"[red]{Icons.FailedIcon} Failed:[/]   {failed}");
        
        return table;
    }
    public static Panel GetFolderPathPanel(string path)
    {
        return new Panel($"[grey]{path}[/]")
        {
            Header = new PanelHeader($"{Icons.FolderIcon} Repository Path", Justify.Left),
            Border = BoxBorder.Rounded,
            Padding = new Padding(1, 0, 1, 0),
            BorderStyle = new Style(Color.Grey)
        };
    }

    public static string RepoSkipped(string repositoryName)
        => $"{Icons.FastForwardIcon}  [yellow]{repositoryName} Skipped[/]";
    
    public static string GetRepoName(string repositoryName)
        => $"{Icons.GearIcon} Repository: {repositoryName}";
    public static string GetPullingWithRepoName(string repositoryName)
        => $"{Icons.GearIcon}Pulling {repositoryName}...";
    public static string GetFetchingWithRepoName(string repositoryName)
        => $"{Icons.GearIcon}Fetching {repositoryName}...";
    
    public static string DeletingExistingPath(string path)
        => $"{Icons.BroomIcon} Deleting existing directory: {path}";
    
    
    public static Rule InitCommandRule()
        => new($"[yellow]{Icons.RocketIcon} Init Roo[/]");
    public static Rule CloneCommandRule()
        => new Rule($"[yellow]{Icons.RocketIcon} Clone[/]");
    public static Rule StatusCommandRule()
        => new Rule($"[yellow]{Icons.StatisticIcon} Status[/]");
    public static Rule FetchCommandRule()
        => new Rule($"[yellow]{Icons.FetchIcon} Fetch[/]");
    public static Rule PullCommandRule()
        => new Rule($"[yellow]{Icons.PullIcon} Pull[/]");
    public static Rule PushCommandRule()
        => new Rule($"[yellow]{Icons.PushIcon} Push[/]");
    public static Rule CheckoutCommandRule()
        => new Rule($"[yellow]{Icons.BranchIcon} Switch Branch[/]");
    public static Rule StashCommandRule()
        => new Rule($"[yellow]{Icons.StashIcon} Stash[/]");
    
    
    public static Rule EmptyRule()
        => new Rule();
    public static Rule GreyDimRule()
        => new Rule().RuleStyle("grey dim");

    public static Rule GetCloneStatisticRule()
        => new Rule($"{Icons.StatisticIcon} Clone Summary").LeftJustified();
    public static Error GetRepoUrlMissingError(string repositoryName)
        => Error.Conflict($"[red] {repositoryName}: Url is missing[/]");
}