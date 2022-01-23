using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;
using OpenStore.Omnichannel.Shared.Query.Management.ProductContext;
using OpenStore.Omnichannel.Shared.Request;
using OpenStore.Omnichannel.Shared.Request.ProductContext;
using OpenStore.Shared;

namespace OpenStore.Omnichannel.Api.Management;

[Route("api/[controller]")]
[RequiresStoreAuthorize]
public class ProductsController : BaseApiController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Guid> CreateProduct(ProductDto model) => _mediator.Send(new CreateProduct(model), CancellationToken);

    [HttpPut("{id:guid}")]
    public Task UpdateProduct(Guid id, ProductDto model) => _mediator.Send(new UpdateProduct(id, model), CancellationToken);

    [HttpPost("{id:guid}/archive")]
    public Task ArchiveProduct(Guid id) => _mediator.Send(new ArchiveProduct(id), CancellationToken);

    [HttpPost("{id:guid}/un-archive")]
    public Task UnArchiveProduct(Guid id) => _mediator.Send(new UnArchiveProduct(id), CancellationToken);

    [HttpDelete("{id:guid}")]
    public Task DeleteProduct(Guid id) => _mediator.Send(new DeleteProduct(id), CancellationToken);

    [HttpGet("{id:guid}")]
    public Task<ProductDto> GetProduct(Guid id) => _mediator.Send(new GetProductForUpdateQuery(id), CancellationToken);

    [HttpGet("all")]
    public Task<PagedList<ProductListItemDto>> GetAllProducts([FromQuery] PageRequestQueryModel pageRequest)
        => _mediator.Send(new GetAllProductsQuery(pageRequest, null), CancellationToken);

    [HttpGet("active")]
    public Task<PagedList<ProductListItemDto>> GetActiveProducts([FromQuery] PageRequestQueryModel pageRequest)
        => _mediator.Send(new GetAllProductsQuery(pageRequest, ProductStatus.Active), CancellationToken);

    [HttpGet("draft")]
    public Task<PagedList<ProductListItemDto>> GetDraftProducts([FromQuery] PageRequestQueryModel pageRequest)
        => _mediator.Send(new GetAllProductsQuery(pageRequest, ProductStatus.Draft), CancellationToken);

    [HttpGet("archived")]
    public Task<PagedList<ProductListItemDto>> GetArchivedProducts([FromQuery] PageRequestQueryModel pageRequest)
        => _mediator.Send(new GetAllProductsQuery(pageRequest, ProductStatus.Archived), CancellationToken);

    [HttpPost("{id:guid}/assign-media")]
    public Task<IEnumerable<ProductMediaDto>> AssignProductMedia(Guid id, IEnumerable<FileUploadDto> model) =>
        _mediator.Send(new AssignProductMedia(id, model), CancellationToken);

    [HttpPost("{id:guid}/update-medias")]
    public Task UpdateProductMedias(Guid id, IEnumerable<ProductMediaDto> medias) => _mediator.Send(new UpdateProductMedias(id, medias), CancellationToken);

    [HttpDelete("{id:guid}/medias/{productMediaId:guid}")]
    public Task DeleteProductMedia(Guid id, Guid productMediaId) => _mediator.Send(new DeleteProductMedia(id, productMediaId), CancellationToken);

    [HttpPost("{id:guid}/variants/update-prices")]
    public Task UpdateProductVariantPrices(Guid id, UpdateVariantPricesRequest request)
        => _mediator.Send(new UpdateProductVariantPrices(id, request.Variants.Select(x => new UpdateProductVariantPrice(x.VariantId, x.Price, x.CompareAtPrice, x.Cost))),
            CancellationToken);

    [HttpPost("{id:guid}/variants/update-stocks")]
    public Task UpdateProductVariantQuantities(Guid id, UpdateVariantStocksRequest request)
        => _mediator.Send(new UpdateProductVariantQuantities(id, request.Variants.Select(x => new UpdateProductVariantQuantity(x.VariantId, x.Quantity))), CancellationToken);

    [HttpPost("{id:guid}/variants/update-barcodes")]
    public Task UpdateProductVariantBarcodes(Guid id, UpdateVariantBarcodesRequest request)
        => _mediator.Send(new UpdateProductVariantBarcodes(id, request.Variants.Select(x => new UpdateProductVariantBarcode(x.VariantId, x.Barcode))), CancellationToken);

    [HttpPost("{id:guid}/variants/update-skus")]
    public Task UpdateProductVariantSkus(Guid id, UpdateVariantSkusRequest request)
        => _mediator.Send(new UpdateProductVariantSkus(id, request.Variants.Select(x => new UpdateProductVariantSku(x.VariantId, x.Sku))), CancellationToken);

    [HttpPost("{id:guid}/variants")]
    public Task<Guid> CreateVariant(Guid id, VariantDto model) => _mediator.Send(new CreateVariant(id, model), CancellationToken);

    [HttpPut("{id:guid}/variants/{variantId:guid}")]
    public Task UpdateProductVariant(Guid id, Guid variantId, VariantDto model) => _mediator.Send(new UpdateProductVariant(id, variantId, model), CancellationToken);

    [HttpPost("{id:guid}/variants/delete-bulk")]
    public Task DeleteVariants(Guid id, IEnumerable<Guid> model) => _mediator.Send(new DeleteVariants(id, model), CancellationToken);

    [HttpPost("{id:guid}/make-multi-variant")]
    public Task<IEnumerable<Guid>> MakeProductAsMultiVariant(Guid id, MakeProductAsMultiVariantRequest request) =>
        _mediator.Send(new MakeProductAsMultiVariant(id, request.Options, request.Variants), CancellationToken);

    [HttpPost("{id:guid}/variants/{variantId:guid}/change-variant-media/{mediaId:guid}")]
    public Task SaveVariantMedia(Guid id, Guid variantId, Guid mediaId) => _mediator.Send(new ChangeVariantMedia(id, variantId, mediaId), CancellationToken);
}