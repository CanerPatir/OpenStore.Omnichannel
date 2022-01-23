using MediatR;
using OpenStore.Omnichannel.Shared.Command;

namespace OpenStore.Omnichannel.Application.Command;

/// <summary>
/// Marker interface for Open Store command handler which does not return value
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

/// <summary>
/// Marker interface for Open Store command handler which returns value
/// </summary>
/// <typeparam name="TCommand">Type of Open Store command</typeparam>
/// <typeparam name="TCommandResult">Type of Open Store command result</typeparam>
public interface ICommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult>
    where TCommand : ICommand<TCommandResult>
{
}