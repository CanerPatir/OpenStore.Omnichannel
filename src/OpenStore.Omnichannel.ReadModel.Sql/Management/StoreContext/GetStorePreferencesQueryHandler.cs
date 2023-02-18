using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Data.EntityFramework.ReadOnly;
using OpenStore.Omnichannel.Domain.StoreContext;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Management.StoreContext;

public class GetStorePreferencesQueryHandler : IQueryHandler<GetStorePreferencesQuery, StorePreferencesQueryResult>
{
    private readonly IReadOnlyRepository<StorePreferences> _repository;
    private readonly IOpenStoreObjectMapper _mapper;

    public GetStorePreferencesQueryHandler(IReadOnlyRepository<StorePreferences> repository, IOpenStoreObjectMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StorePreferencesQueryResult> Handle(GetStorePreferencesQuery request, CancellationToken cancellationToken)
    {
        var storePreferences = await _repository.Query.FirstOrDefaultAsync(cancellationToken);
        if (storePreferences is null)
        {
            // storePreferences = new StorePreferences();
            // storePreferences = await _repository.InsertAsync(storePreferences, cancellationToken);
            // await _repository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<StorePreferencesQueryResult>(storePreferences);
    }
}