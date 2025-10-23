namespace Roo.Cli.Common;

public interface IPromptHandler
{
    public PromptAnswer PromptYesNo(string message);
}
public class PromptHandler : IPromptHandler
{
    private readonly IRooLogger _logger;
    private readonly IRooUserInput _userInput;
    private static readonly HashSet<string> YesOptions = new(StringComparer.OrdinalIgnoreCase) { "y", "ye", "yes" };
    private static readonly HashSet<string> NoOptions = new(StringComparer.OrdinalIgnoreCase) { "n", "no" };

    public PromptHandler(IRooLogger logger, IRooUserInput userInput)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userInput = userInput ?? throw new ArgumentNullException(nameof(userInput));
    }
    public PromptAnswer PromptYesNo(string message)
    {
        _logger.LogWithNoNewLine($"{message} [y/N]: ");
        while (true)
        {
            var input = _userInput.ReadInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                return PromptAnswer.No;
            }
            if (YesOptions.Contains(input))
            {
                return PromptAnswer.Yes;
            }
            if (NoOptions.Contains(input))
            {
                return PromptAnswer.No;
            }
            
            // Invalid input, retry
            _logger.Log("\nPlease enter 'yes/y' or 'no/n': ");        
        }
    }
}

public enum PromptAnswer
{
    Yes,
    No,
}
