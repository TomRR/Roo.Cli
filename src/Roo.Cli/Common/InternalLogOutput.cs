namespace Roo.Cli.Common;

public static class InternalLogOutput
{
    public static string Output = string.Empty;

    public static void GetConsoleLog(string output)
    {
        Output = Output +  output + "\n";
    }
}