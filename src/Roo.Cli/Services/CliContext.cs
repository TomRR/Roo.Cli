namespace Roo.Cli.Services;

public class CliContext
{
    public string? Command { get; set; } = null;
    public List<string> Arguments { get; set; } = new();
    public Dictionary<string, string> Options { get; set; } = new();
}