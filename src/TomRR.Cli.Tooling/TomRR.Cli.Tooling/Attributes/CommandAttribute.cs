namespace TomRR.Cli.Tooling.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CommandAttribute : Attribute
{
    public string? Name { get; }
    public string? ShortName { get; }
    
    public string? Description { get; }
    
    public ServiceLifetime? DependencyLifetime { get; }

    public CommandAttribute() { }

    public CommandAttribute(string? name = null, string? shortName = null, string? description = null, ServiceLifetime? dependencyLifetime = null)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
        DependencyLifetime = dependencyLifetime;
    }
}