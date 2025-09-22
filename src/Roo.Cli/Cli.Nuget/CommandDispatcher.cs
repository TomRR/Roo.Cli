namespace Roo.Cli.Cli.Nuget;

public sealed class CommandDispatcher
{
    private readonly IServiceProvider _provider;
    private readonly Dictionary<string, Type> _commands = new();

    public static CommandDispatcher Instance { get; } = new CommandDispatcher();

    public static CommandDispatcher Create => new CommandDispatcher();

    public CommandDispatcher()
    {
        
    }
    public CommandDispatcher(IServiceProvider provider)
    {
        _provider = provider;

        var commandTypes = provider.GetServices<ICommand>().Select(c => c.GetType());

        foreach (var type in commandTypes)
        {
            var attr = type.GetCustomAttributes(typeof(CommandAttribute), false)
                .OfType<CommandAttribute>()
                .FirstOrDefault();
            if (attr != null)
            {
                _commands[attr.Name] = type;
                if (!string.IsNullOrEmpty(attr.ShortName))
                    _commands[attr.ShortName!] = type;
            }
        }
    }

    public async Task DispatchAsync(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Available commands: " + string.Join(", ", _commands.Keys));
            return;
        }

        var commandName = args[0];
        if (_commands.TryGetValue(commandName, out var type))
        {
            var cmd = (ICommand)_provider.GetRequiredService(type);
            await cmd.RunAsync(args.Skip(1).ToArray());
        }
        else
        {
            Console.WriteLine($"Unknown command: {commandName}");
        }
    }
}
