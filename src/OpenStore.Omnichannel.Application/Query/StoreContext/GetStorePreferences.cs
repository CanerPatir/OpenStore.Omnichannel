using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.StoreContext;
using OpenStore.Omnichannel.Shared.Dto.Store;

namespace OpenStore.Omnichannel.Application.Query.StoreContext;

public record GetStorePreferences : IRequest<StorePreferencesDto>;

public class GetStorePreferencesHandler : IRequestHandler<GetStorePreferences, StorePreferencesDto>
{
    private readonly ICrudRepository<StorePreferences> _repository;
    private readonly IOpenStoreObjectMapper _mapper;

    public GetStorePreferencesHandler(ICrudRepository<StorePreferences> repository, IOpenStoreObjectMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StorePreferencesDto> Handle(GetStorePreferences request, CancellationToken cancellationToken)
    {
        var storePreferences = await _repository.Query.FirstOrDefaultAsync(cancellationToken);
        if (storePreferences is null)
        {
            storePreferences = new StorePreferences();
            storePreferences = await _repository.InsertAsync(storePreferences, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<StorePreferencesDto>(storePreferences);
    }
}