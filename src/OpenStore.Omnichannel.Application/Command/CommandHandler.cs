using MediatR;
using OpenStore.Omnichannel.Shared.Command;

namespace OpenStore.Omnichannel.Application.Command;

/// <summary>
/// Marker abstract class for Open Store command handler which does not return value
/// </summary>
/// <typeparam name="TCommand">Type of Open Store command</typeparam>
public abstract class CommandHandler<TCommand> : AsyncRequestHandler<TCommand> where TCommand : ICommand
{
}