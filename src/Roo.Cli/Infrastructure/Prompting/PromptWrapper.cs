namespace Roo.Cli.Infrastructure.Prompting;

public interface IPromptWrapper
{
    public string AskForString(string question);
    public int AskForInt(string question);
    public void AskOptionalQuestion(string question);
    public void Ask(string question, string[] choices, string defaultAnswer);
    public string AskWithValidation(string message, string defaultValue, Func<string, bool> validationCondition,
        string validationErrorMessage);
    public string SingleSelect(string question, string[] choices, int pageSize, string moreChoicesText);
    public List<string> MultiSelect(string question, string[] choices, int pageSize, string moreChoicesText,
        string instructionsText);
}

public sealed class PromptWrapper : IPromptWrapper
{
    public string AskForString(string question) 
        => AnsiConsole.Prompt(new TextPrompt<string>(question));
    public int AskForInt(string question) 
        => AnsiConsole.Prompt(new TextPrompt<int>(question));
    public void AskOptionalQuestion(string question) 
        => AnsiConsole.Prompt(
            new TextPrompt<string>(question)
                .AllowEmpty());
    public void Ask(string question, string[] choices, string defaultAnswer) 
        => AnsiConsole.Prompt(
            new TextPrompt<string>(question)
                .AddChoices(choices)
                .DefaultValue(defaultAnswer));
    public string AskWithValidation(string message, string defaultValue, Func<string, bool> validationCondition, string validationErrorMessage) 
        => AnsiConsole.Prompt(
            new TextPrompt<string>(message)
                .DefaultValue(defaultValue)
                .Validate(value => validationCondition(value) 
                    ? ValidationResult.Success() 
                    : ValidationResult.Error(validationErrorMessage)));
    public string SingleSelect(string question, string[] choices, int pageSize, string moreChoicesText) 
        => AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(question)
                .PageSize(pageSize)
                .MoreChoicesText(moreChoicesText)
                .AddChoices(choices));
    public List<string> MultiSelect(string question, string[] choices, int pageSize, string moreChoicesText, string instructionsText) 
        => AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title(question)
                .PageSize(pageSize)
                .MoreChoicesText(moreChoicesText)
                .InstructionsText(instructionsText)
                .AddChoices(choices));
}