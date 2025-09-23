namespace TomRR.Cli.Tooling.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class ArgumentAttribute : Attribute
{
    public int Position { get; }
    public ArgumentAttribute(int position) => Position = position;
}