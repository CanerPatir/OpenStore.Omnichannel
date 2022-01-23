using OpenStore.Application.Crud;
using OpenStore.Application.Exceptions;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Application;

internal static class RepositoryExtensions
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

}