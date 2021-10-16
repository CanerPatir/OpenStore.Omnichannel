using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Application.Extensions;

internal static class RepositoryQueryExtensions
{
    public static async Task<T> GetRequired<T>(this ICrudRepository<T> repository, Guid id, CancellationToken cancellationToken = default)
        where T : class, IEntity
    {
        var entity = await repository.GetAsync(id, cancellationToken);

        if (entity == default)
        {
            throw new ResourceNotFoundException("Required resource not found");
        }

        return entity;
    }

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
            .OrderBy(x => x.Id);

        var items = await query
            // .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new PagedList<TDto>(items.Select(mapper), count, pageNumber, pageSize);
    }
}