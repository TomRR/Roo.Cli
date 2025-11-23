namespace Roo.Cli.Features.Commands.Core;

public interface ICommandHandler<TRequest>
{
    Task<Result<IRooCommandResponse, Error, Skipped>> HandleAsync(TRequest request);
}


// public interface ICommandRequest
// {
//     // Optional: common command properties
// }
//
// public interface ICommandHandler<TRequest> where TRequest : ICommandRequest
// {
//     Task<Result<RooCommandResponse, Error, Skipped>> HandleAsync(TRequest request);
// }
//
// public interface ICommandHandler<TRequest, TResponse>
//     where TRequest : ICommandRequest
//     where TResponse : RooCommandResponse
// {
//     Task<Result<TResponse, Error, Skipped>> HandleAsync(TRequest request);
// }
