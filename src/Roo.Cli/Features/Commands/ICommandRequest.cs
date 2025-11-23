namespace Roo.Cli.Features.Commands;

public interface ICommandRequest
{
    public string CommandName { get; }
    public bool Interactive { get; }
    public bool Select { get; }
    public bool MultiSelect { get; }
}