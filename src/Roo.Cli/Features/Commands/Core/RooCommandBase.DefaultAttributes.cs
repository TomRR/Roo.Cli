namespace Roo.Cli.Features.Commands.Core;

public abstract partial class RooCommandBase
{
    [Option("--interactive", "-i", hasValue: false)]
    public bool Interactive { get; set; }
    
    [Option("--select", "-s", hasValue: false)]
    public bool Select { get; set; }
    
    [Argument(ofOption: nameof(Select), 0)]
    public string? SelectedRepositoryName { get; set; }
    
    [Option("--multi-select", "-ms", hasValue: false)]
    public bool MultiSelect { get; set; }
    
    [Argument(ofOption: nameof(MultiSelect), 0)]
    public string? MultiSelectedRepositoryNames { get; set; }
}