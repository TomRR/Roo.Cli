namespace Roo.Cli.Common.Models;

public enum CommandResultType
{
    Success,
    Skipped,
    Failed
}

public static class CommandResultTypeExtensions
{
    public static string ToIcon(this CommandResultType source)
    {
        return source switch
        {
            CommandResultType.Success => Icons.CheckIcon,
            CommandResultType.Skipped => Icons.FastForwardIcon,
            CommandResultType.Failed => Icons.ErrorIcon,
            _ => ""
        };
    }
}