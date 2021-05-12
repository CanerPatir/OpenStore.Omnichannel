using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Application;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Application.Command;
using OpenStore.Omnichannel.Application.Query;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Product;
using OpenStore.Omnichannel.Shared.ReadModel;
using OpenStore.Omnichannel.Shared.Request;

namespace OpenStore.Omnichannel.Api.Store
{
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

        [HttpPost("{id:guid}/archive")]
        public Task ArchiveProduct(Guid id) => _mediator.Send(new ArchiveProduct(id), CancellationToken);

        [HttpPost("{id:guid}/un-archive")]
        public Task UnArchiveProduct(Guid id) => _mediator.Send(new UnArchiveProduct(id), CancellationToken);

        [HttpDelete("{id:guid}")]
        public Task DeleteProduct(Guid id) => _mediator.Send(new DeleteProduct(id), CancellationToken);

        [HttpGet("{id:guid}")]
        public Task<ProductDto> GetProduct(Guid id) => _mediator.Send(new GetProductForUpdate(id), CancellationToken);

        [HttpGet("all")]
        public Task<PagedList<ProductListItemReadModel>> GetAllProducts([FromQuery] PageRequestQueryModel pageRequest)
            => _mediator.Send(new GetAllProducts(pageRequest, null), CancellationToken);

        [HttpGet("active")]
        public Task<PagedList<ProductListItemReadModel>> GetActiveProducts([FromQuery] PageRequestQueryModel pageRequest)
            => _mediator.Send(new GetAllProducts(pageRequest, ProductStatus.Active), CancellationToken);

        [HttpGet("draft")]
        public Task<PagedList<ProductListItemReadModel>> GetDraftProducts([FromQuery] PageRequestQueryModel pageRequest)
            => _mediator.Send(new GetAllProducts(pageRequest, ProductStatus.Draft), CancellationToken);

        [HttpGet("archived")]
        public Task<PagedList<ProductListItemReadModel>> GetArchivedProducts([FromQuery] PageRequestQueryModel pageRequest)
            => _mediator.Send(new GetAllProducts(pageRequest, ProductStatus.Archived), CancellationToken);

        [HttpPost("{id:guid}/assign-media")]
        public Task<IEnumerable<ProductMediaDto>> AssignProductMedia(Guid id, IEnumerable<FileUploadDto> model) =>
            _mediator.Send(new AssignProductMedia(id, model), CancellationToken);

        [HttpPost("{id:guid}/update-medias")]
        public Task UpdateProductMedias(Guid id, IEnumerable<ProductMediaDto> medias) => _mediator.Send(new UpdateProductMedias(id, medias), CancellationToken);

        [HttpDelete("{id:guid}/medias/{productMediaId:guid}")]
        public Task DeleteProductMedia(Guid id, Guid productMediaId) => _mediator.Send(new DeleteProductMedia(id, productMediaId), CancellationToken);

        [HttpPost("{id:guid}/variants/update-prices")]
        public Task UpdateProductVariantPrices(Guid id, UpdateVariantPricesRequest request)
            => _mediator.Send(new UpdateProductVariantPrices(id, request.Variants.Select(x => new UpdateProductVariantPrice(x.VariantId, x.Price, x.CompareAtPrice, x.Cost))), CancellationToken);

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
    }
}