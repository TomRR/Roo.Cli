namespace Roo.Cli.Cli.Nuget.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class OptionAttribute : Attribute
{
    public string LongName { get; }
    public string? ShortName { get; }
    public bool HasValue { get; }

    public OptionAttribute(string longName, string? shortName = null, bool hasValue = true)
    {
        LongName = longName;
        ShortName = shortName;
        HasValue = hasValue;
    }
}