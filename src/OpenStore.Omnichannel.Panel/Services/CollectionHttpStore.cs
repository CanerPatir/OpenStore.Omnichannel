using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Panel.Services;

public class CollectionHttpStore : HttpStore
{
    public CollectionHttpStore(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override string Path => "api/collections";

    public Task<PagedList<CollectionListItemDto>> GetAll(PageRequest request) => HttpClient.GetPage<CollectionListItemDto>(Path, request);

    public async Task<Guid> Create(ProductCollectionDto model)
    {
        return Guid.Empty;
    }
    
    public async Task<ProductCollectionDto> Get(Guid id)
    {
        return new ProductCollectionDto();
    }
}