using MediatR;

namespace OpenStore.Omnichannel.Shared.Query;

/// <summary>
/// Marker interface for Open Store queries
/// </summary>
/// /// <typeparam name="TQueryResult">Type of Open Store query result</typeparam>
public interface IQuery<out TQueryResult> : IRequest<TQueryResult>
{
}