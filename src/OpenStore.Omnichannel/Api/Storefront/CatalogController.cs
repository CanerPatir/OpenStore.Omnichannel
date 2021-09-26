using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.ReadModel.Query;
using OpenStore.Omnichannel.ReadModel.Query.Result;

namespace OpenStore.Omnichannel.Api.Storefront
{
    [Microsoft.AspNetCore.Components.Route("api-sf/[controller]")]
    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

       [HttpGet("product-detail/{id:guid}")]
       public Task<GetProductDetailResult> GetProductDetail([FromRoute] Guid id) => _mediator.Send(new GetProductDetailQuery());
    }
}