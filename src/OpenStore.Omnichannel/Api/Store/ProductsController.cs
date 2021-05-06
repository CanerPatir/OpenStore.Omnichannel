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
        
        [HttpGet("all")]
        public Task<PagedList<ProductListItemReadModel>> AllProducts([FromQuery] int? pageNumber, [FromQuery] int? pageSize) 
            => _mediator.Send(new GetAllProducts(pageNumber, pageSize), CancellationToken);
    }
}