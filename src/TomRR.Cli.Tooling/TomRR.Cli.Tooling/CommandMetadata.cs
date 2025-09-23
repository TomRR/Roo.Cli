// namespace TomRR.Cli.Tooling;
//
// internal record CommandOption(PropertyInfo Property, OptionAttribute Attribute);
// internal record CommandArgument(PropertyInfo Property, ArgumentAttribute Attribute);
//
// internal class CommandMetadata
// {
//     public List<CommandOption> Options { get; } = new();
//     public List<CommandArgument> Arguments { get; } = new();
//
//     public static CommandMetadata FromType(Type type)
//     {
//         var metadata = new CommandMetadata();
//         foreach (var prop in type.GetProperties())
//         {
//             if (prop.GetCustomAttribute<OptionAttribute>() is { } opt)
//                 metadata.Options.Add(new CommandOption(prop, opt));
//             if (prop.GetCustomAttribute<ArgumentAttribute>() is { } arg)
//                 metadata.Arguments.Add(new CommandArgument(prop, arg));
//         }
//         return metadata;
//     }
// }