using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Api.Management;

[Route("api/[controller]")]
[RequiresStoreAuthorize]
public class CollectionsController : BaseApiController
{
    private readonly IMediator _mediator;

    public CollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public Task<PagedList<CollectionListItemDto>> GetAllProductCollections([FromQuery] PageRequestQueryModel pageRequest)
        => _mediator.Send(new GetAllCollections(pageRequest), CancellationToken);
    
    [HttpPost]
    public Task<Guid> Create(ProductCollectionDto model)
        => _mediator.Send(new CreateProductCollection(model), CancellationToken);
    
    [HttpGet("{id:guid}")]
    public Task<ProductCollectionDto> GetProductCollection(Guid id) => _mediator.Send(new GetProductCollectionForUpdate(id), CancellationToken);
    
    [HttpPut("{id:guid}")]
    public Task UpdateProductCollection(Guid id, ProductCollectionDto model) => _mediator.Send(new UpdateProductCollection(id, model), CancellationToken);
}