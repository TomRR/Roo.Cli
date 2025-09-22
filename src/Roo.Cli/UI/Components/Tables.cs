using Roo.Cli.Features.Commands;

namespace Roo.Cli.UI.Components;

public static partial class Components
{
    public static class Tables
    {
        public static Table GetCloningResultTable(List<CliResults> results)
        {
            var success = results.Count(r => r == CliResults.Success);
            var skipped = results.Count(r => r == CliResults.Skipped);
            var failed = results.Count(r => r == CliResults.Failed);
        
            var table = new Table().Border(TableBorder.None);
            table.AddColumn(new TableColumn(""));
            table.AddRow($"[green]{Icons.CheckIcon} Cloned:[/]   {success}");
            table.AddRow($"[yellow]{Icons.WarningIcon}Skipped:[/]  {skipped}");
            table.AddRow($"[red]{Icons.FailedIcon} Failed:[/]   {failed}");
        
            return table;
        }
    }
}