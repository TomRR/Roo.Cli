namespace Roo.Cli.Features.Commands.Core;

/// <summary>
    /// Fluent builder for CLI command arguments.
    /// Handles flags, options, positional arguments, and lists.
    /// </summary>
    public sealed class CliCommandBuilder
    {
        private readonly List<string> _args = new();

        private CliCommandBuilder() { }

        /// <summary>
        /// Creates a new instance of <see cref="CliCommandBuilder"/>.
        /// </summary>
        public static CliCommandBuilder Create() => new CliCommandBuilder();

        /// <summary>
        /// Adds a raw argument if not null or whitespace.
        /// </summary>
        public CliCommandBuilder Add(string? arg)
        {
            if (string.IsNullOrWhiteSpace(arg)) return this;
            _args.Add(arg.Trim());
            return this;
        }

        /// <summary>
        /// Adds a range of arguments.
        /// Null or whitespace arguments are ignored.
        /// </summary>
        public CliCommandBuilder AddRange(IEnumerable<string>? args)
        {
            if (args == null) return this;
            foreach (var a in args) Add(a);
            return this;
        }

        /// <summary>
        /// Adds a boolean flag like --force (only name; no value)
        /// </summary>
        public CliCommandBuilder AddFlag(string flag, bool when = true)
        {
            if (when) Add(flag);
            return this;
        }

        /// <summary>
        /// Adds an option with a single value: --remote origin
        /// </summary>
        /// <param name="name">The option name (e.g., "--set-upstream"). Cannot be null or whitespace.</param>
        /// <param name="value">The option value. Ignored if null or whitespace.</param>
        public CliCommandBuilder AddOption(string name, string? value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Option name cannot be null or empty.", nameof(name));

            if (!string.IsNullOrWhiteSpace(value))
            {
                Add(name);
                Add(value);
            }
            return this;
        }

        /// <summary>
        /// Adds a positional argument.
        /// Null or whitespace values are ignored.
        /// </summary>
        public CliCommandBuilder AddPositional(string? value)
        {
            return Add(value);
        }

        /// <summary>
        /// Returns a read-only list of arguments in order of addition.
        /// </summary>
        public IReadOnlyList<string> Build() => _args.AsReadOnly();

        /// <summary>
        /// Returns a single string of all arguments, space-separated.
        /// Useful for logging or debugging.
        /// </summary>
        public string BuildAsString() => string.Join(' ', _args);
    }