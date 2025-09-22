namespace Roo.Cli.Cli.Nuget;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class CommandAttribute : Attribute
{
    public string Name { get; }
    public string? ShortName { get; }

    public CommandAttribute(string name, string? shortName = null)
    {
        Name = name;
        ShortName = shortName;
    }
}