using MediatR;
using OpenStore.Omnichannel.Shared.Query;

namespace OpenStore.Omnichannel.ReadModel.Sql;

/// <summary>
/// Marker interface for Open Store query handlers
/// </summary>
/// <typeparam name="TQuery">Type of Open Store query</typeparam>
/// <typeparam name="TQueryResult">Type of Open Store query result</typeparam>
public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
{
}