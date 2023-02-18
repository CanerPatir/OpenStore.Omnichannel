using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Domain;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.ReadModel.Sql;

internal static class RepositoryQueryExtensions
{
    public static async Task<PagedList<TDto>> GetPaged<T, TDto>(this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        Func<T, TDto> mapper,
        CancellationToken cancellationToken = default)
        where T : class, IEntity
    {
        var count = await query.CountAsync(cancellationToken);

        query = query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            // .OrderBy(x => x.Id)
            ;

        var items = await query
            .ToListAsync(cancellationToken);

        return new PagedList<TDto>(items.Select(mapper), count, pageNumber, pageSize);
    }
}