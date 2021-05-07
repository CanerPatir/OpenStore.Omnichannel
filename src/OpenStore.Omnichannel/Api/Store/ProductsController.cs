using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Application;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Application.Query;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto.Product;
using OpenStore.Omnichannel.Shared.ReadModel;

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
        
        [HttpGet("{id:guid}")]
        public Task<ProductDto> GetProduct(Guid id) => _mediator.Send(new GetProductForUpdate(id), CancellationToken);
        
        [HttpPost("all")]
        public Task<PagedList<ProductListItemReadModel>> GetAllProducts(PageRequest pageRequest) 
            => _mediator.Send(new GetAllProducts(pageRequest, null, false), CancellationToken);
        
        [HttpPost("active")]
        public Task<PagedList<ProductListItemReadModel>> GetActiveProducts(PageRequest pageRequest) 
            => _mediator.Send(new GetAllProducts(pageRequest, ProductStatus.Active, false), CancellationToken);
        
        [HttpPost("draft")]
        public Task<PagedList<ProductListItemReadModel>> GetDraftProducts(PageRequest pageRequest) 
            => _mediator.Send(new GetAllProducts(pageRequest, ProductStatus.Draft, false), CancellationToken);
        
        [HttpPost("deleted")]
        public Task<PagedList<ProductListItemReadModel>> GetDeletedProducts(PageRequest pageRequest) 
            => _mediator.Send(new GetAllProducts(pageRequest, null, true), CancellationToken);
    }
}