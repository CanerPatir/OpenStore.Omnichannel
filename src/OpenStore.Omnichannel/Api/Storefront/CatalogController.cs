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

    [HttpGet("product/{handle}")]
    public Task<ProductDetailResult> GetProductDetail(string handle) => _mediator.Send(new GetProductDetailByHandleQuery(handle), CancellationToken);

    [HttpGet("all/{batchSize:int}/{firstIndex:int}")]
    public Task<AllProductsResult> GetAllProducts(int batchSize, int firstIndex) => _mediator.Send(new GetAllProductsQuery(batchSize, firstIndex), CancellationToken);

    [HttpGet("collection/{id:guid}/{batchSize:int}/{firstIndex:int}")]
    public Task<CollectionProductsResult> GetCollectionProducts(Guid id, int batchSize, int firstIndex) =>
        _mediator.Send(new GetCollectionProductsQuery(id, batchSize, firstIndex), CancellationToken);
}