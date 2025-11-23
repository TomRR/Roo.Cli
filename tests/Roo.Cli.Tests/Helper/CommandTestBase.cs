using System.Windows.Input;

namespace Roo.Cli.Tests.Helper;

// public abstract class CommandTestBase
// {
//     protected RooCommandContext CreateContext() => new RooCommandContext(
//         Substitute.For<IRooLogger>(),
//         Substitute.For<IRooConfigService>(),
//         Substitute.For<IDirectoryService>(),
//         Substitute.For<IRooCommandValidator>()
//     );
//
//     protected ICommandAction<T> CreateAction<T>() where T : ICommand
//         => Substitute.For<ICommandAction<T>>();
// }