// namespace TomRR.Cli.Tooling;
//
// public static class CommandBinder
// {
//     public static void Bind(ICommand command, string[] args)
//     {
//         var type = command.GetType();
//         var metadata = CommandMetadata.FromType(type);
//
//         var queue = new Queue<string>(args);
//         int argPosition = 0;
//
//         while (queue.Count > 0)
//         {
//             var token = queue.Dequeue();
//
//             if (token.StartsWith("--") || token.StartsWith("-"))
//             {
//                 var option = metadata.Options.FirstOrDefault(o =>
//                     o.Attribute.LongName == token || o.Attribute.ShortName == token);
//
//                 if (option is not null)
//                 {
//                     if (option.Attribute.HasValue)
//                     {
//                         if (queue.Count == 0)
//                             throw new ArgumentException($"Option {token} requires a value");
//
//                         var value = queue.Dequeue();
//                         option.Property.SetValue(command, Convert.ChangeType(value, option.Property.PropertyType));
//                     }
//                     else
//                     {
//                         option.Property.SetValue(command, true);
//                     }
//                 }
//             }
//             else
//             {
//                 var argProp = metadata.Arguments
//                     .FirstOrDefault(a => a.Attribute.Position == argPosition);
//                 if (argProp is not null)
//                 {
//                     argProp.Property.SetValue(command, Convert.ChangeType(token, argProp.Property.PropertyType));
//                 }
//                 argPosition++;
//             }
//         }
//     }
// }
//
