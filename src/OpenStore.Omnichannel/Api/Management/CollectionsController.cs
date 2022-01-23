using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management;
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
        => _mediator.Send(new GetAllCollectionsQuery(pageRequest), CancellationToken);
    
    [HttpPost]
    public Task<Guid> Create(ProductCollectionDto model)
        => _mediator.Send(new CreateProductCollection(model), CancellationToken);
    
    [HttpGet("{id:guid}")]
    public Task<ProductCollectionDto> GetProductCollection(Guid id) => _mediator.Send(new GetProductCollectionForUpdateQuery(id), CancellationToken);
    
    [HttpPut("{id:guid}")]
    public Task UpdateProductCollection(Guid id, ProductCollectionDto model) => _mediator.Send(new UpdateProductCollection(id, model), CancellationToken);
    
    [HttpDelete("{id:guid}")]
    public Task DeleteProductCollection(Guid id) => _mediator.Send(new DeleteProductCollection(id), CancellationToken);
    
    [HttpGet("{id:guid}/items")]
    public Task<IEnumerable<ProductCollectionItemDto>> GetProductCollectionItems(Guid id) => _mediator.Send(new GetProductCollectionItemsQuery(id), CancellationToken);
    
    [HttpGet("{id:guid}/eligible-items")]
    public Task<IEnumerable<ProductCollectionItemDto>> GetProductCollectionEligibleItems(Guid id, [FromQuery] string term) 
        => _mediator.Send(new GetProductCollectionEligibleItemsQuery(id, term), CancellationToken);

    [HttpPost("{id:guid}/items")]
    public Task AddItemsToCollection(Guid id, IEnumerable<Guid> productIds) => _mediator.Send(new AddItemsToCollection(id, productIds), CancellationToken);
    
    [HttpDelete("{id:guid}/items/{productId:guid}")]
    public Task RemoveProductCollectionItem(Guid id, Guid productId) => _mediator.Send(new RemoveProductCollectionItem(id, productId), CancellationToken);
    
    [HttpPost("{id:guid}/change-image")]
    public Task<ProductCollectionMediaDto> ChangeImage(Guid id, FileUploadDto model) => _mediator.Send(new ChangeProductCollectionImage(id, model), CancellationToken);
    
    [HttpPost("{id:guid}/remove-image")]
    public Task RemoveImage(Guid id) => _mediator.Send(new RemoveProductCollectionImage(id), CancellationToken);

}