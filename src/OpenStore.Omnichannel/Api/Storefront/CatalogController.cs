using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Shared.Query;
using OpenStore.Omnichannel.Shared.Query.Result;

namespace OpenStore.Omnichannel.Api.Storefront;

[Route("api-sf/[controller]")]

public class CatalogController : BaseApiController
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("product-detail/{id:guid}")]
    public Task<ProductDetailResult> GetProductDetail([FromRoute] Guid id) => _mediator.Send(new GetProductDetailQuery(id));

    [HttpGet("all/{batchSize:int}/{firstIndex:int}")]
    public Task<AllProductsResult> GetAllProducts(int batchSize, int firstIndex) => _mediator.Send(new GetAllProductsQuery(batchSize, firstIndex));

    [HttpGet("collection/{id:guid}/{batchSize:int}/{firstIndex:int}")]
    public Task<CollectionProductsResult> GetCollectionProducts(Guid id, int batchSize, int firstIndex) => _mediator.Send(new GetCollectionProductsQuery(id, batchSize, firstIndex));
}