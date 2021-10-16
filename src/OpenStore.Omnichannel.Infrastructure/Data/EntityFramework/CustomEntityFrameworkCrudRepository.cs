namespace OpenStore.Omnichannel.Infrastructure.Data.EntityFramework;

// public class CustomEntityFrameworkCrudService<TEntity, TDto> : EntityFrameworkCrudService<TEntity, TDto>
//     where TEntity : class, IEntity
//     where TDto : class
// {
//     private readonly IMapper _autoMapper;
//
//     public CustomEntityFrameworkCrudService(
//         IMapper autoMapper,
//         ICrudRepository<TEntity> repository,
//         IOpenStoreObjectMapper mapper
//     ) : base(repository, mapper)
//     {
//         _autoMapper = autoMapper;
//     }
//
//     private EntityFrameworkCrudRepository<TEntity> EfRepository => Repository as EntityFrameworkCrudRepository<TEntity>;
//
//     public override Task Update(object id, TDto model, CancellationToken cancellationToken = default)
//     {
//         return EfRepository.Set.Persist(_autoMapper).InsertOrUpdateAsync(model, cancellationToken);
//     }
//
//     public override async Task<object> Create(TDto model, CancellationToken cancellationToken = default)
//     {
//         var entity = await EfRepository.Set.Persist(_autoMapper).InsertOrUpdateAsync(model, cancellationToken);
//         return entity.Id;
//     }
// }