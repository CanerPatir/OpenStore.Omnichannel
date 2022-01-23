using MediatR;

namespace OpenStore.Omnichannel.Shared.Command;

/// <summary>
/// Marker interface for Open Store command without return value
/// </summary>
public interface ICommand : IRequest
{
}

/// <summary>
/// Marker interface for Open Store command with return value
/// </summary>
/// <typeparam name="TCommandResult">Type of Open Store command result</typeparam>
public interface ICommand<out TCommandResult> : IRequest<TCommandResult>
{
}