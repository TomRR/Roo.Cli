// namespace Roo.Cli.Features.Commands.Git.Clone;
//
// [Command("clone")]
// public sealed partial class CloneCommand : RooCommandBase
// {
//     private readonly ICommandAction<CloneCommand> _action;
//     private readonly IRooLogger _logger;
//     private readonly IDirectoryService _directoryService;
//     private readonly IPromptHandler _promptHandler;
//     private readonly List<CliResults> _results = new();
//     public CloneCommand(RooCommandContext context, ICommandAction<CloneCommand> action, IPromptHandler promptHandler)        
//         : base(context)
//     {
//         _logger = context.Logger ?? throw new ArgumentNullException(nameof(context.Logger));
//         _directoryService = context.DirectoryService ?? throw new ArgumentNullException(nameof(context.DirectoryService));
//         _action = action ?? throw new ArgumentNullException(nameof(action));
//         _promptHandler = promptHandler ?? throw new ArgumentNullException(nameof(promptHandler));
//     }
//     
//     [Option("--force", "-f", hasValue: false, description: "Force overwrite existing repositories.")]
//     public bool Force { get; set; }
//     [Option("--clear", "-c", hasValue: false, description: "clear paths before cloning")]
//     public bool Clear { get; set; }
//     
//     protected override bool ShouldSkipRepository(RepositoryDto repository) => false;    
//
//     public override async Task RunAsync()
//     {
//         _logger.LogApplicationNameFiglet();
//         _logger.Log(Components.Rules.CloneCommandRule());
//         
//         await WithRooConfigAsync(RunCommandPerRepositoryAsync);
//         
//         _logger.AddLineBreak();
//         _logger.Log(Components.Rules.GetCloneStatisticRule());
//         _logger.Log(Components.Tables.GetCloningResultTable(_results));
//         _logger.AddLineBreak();
//         _logger.LogTaskCompleted();
//     }
//     
//     private async Task RunCommandPerRepositoryAsync(RepositoryDto repository)
//     {
//         _logger.Log(Components.Messages.GetRepoName(repository.Name));
//         _logger.Log(Components.Panels.GetFolderPathPanel(repository.Path));
//
//         var existingRepoResult = CheckForExistingRepository(repository);
//         if (existingRepoResult.HasError)
//         {
//             var additionalMessage = $"{Icons.ErrorIcon}  [red]Something went wrong with {repository.Name}[/]";
//             _logger.LogError(existingRepoResult.Error, additionalMessage: additionalMessage, additionalLineBreaksAfter: 2);
//             _results.Add(CliResults.Failed);
//             return;
//         }
//
//         if (existingRepoResult  is { IsStatusOnly: true, StatusOnly: Skipped})
//         {
//             _logger.Log(Components.Messages.RepoSkipped(repository.Name), additionalLineBreaksAfter: 1);
//             _results.Add(CliResults.Skipped);
//             return;
//         }
//         
//         
//         _logger.Log(Components.Messages.GetCloningWithRepoName(repository.Url));
//         
//         
//         var args = CliCommandBuilder.Create().Add(CommandName).Build();
//         var request = RooCommandRequest.Create(repository, args);
//         var commandResult = await _action.RunCommandAsync(request);
//         
//         if (commandResult.HasError)
//         {
//             _logger.LogError(commandResult.Error, additionalLineBreaksAfter: 1);
//             _results.Add(CliResults.Failed);
//             return;
//         }
//         
//         _logger.Log($"{Icons.GreenDotIcon} successful cloned repo '{repository.Name}' to '{repository.Path}'", additionalLineBreaksAfter: 1);
//         _results.Add(CliResults.Success);
//     }
//     
//     private Result<Success, Error, Skipped> CheckForExistingRepository(RepositoryDto repository)
//     {
//         if (!_directoryService.DirectoryExists(repository.GetLocalRepoPath()))
//         {
//             return Result.Success;
//         }
//         
//         if (!Force)
//         {
//             var message = $"{Icons.WarningIcon} Path already exists\n Overwrite?";
//             var overwriteAnswer = _promptHandler.PromptYesNo(message);
//             
//             if (overwriteAnswer == PromptAnswer.No)
//             {
//                 return Result.Skipped;
//             }
//         
//         }
//         
//         _logger.Log(Components.Messages.DeletingExistingPath(repository.GetLocalRepoPath()));
//         
//         var deleted = _directoryService.DeleteDirectory(repository.GetLocalRepoPath(), recursive: true);
//         return deleted.IsSuccessful ? deleted.Value : deleted.Error;
//     }
// }