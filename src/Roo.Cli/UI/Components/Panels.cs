namespace Roo.Cli.UI.Components;

public static partial class Components
{
    public static class Panels
    {
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
    }
}