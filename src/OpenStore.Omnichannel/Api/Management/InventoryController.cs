using MediatR;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Application;
using OpenStore.Omnichannel.Application.Query.InventoryContext;
using OpenStore.Omnichannel.Shared.Dto.Inventory;

namespace OpenStore.Omnichannel.Api.Management;

[Route("api/[controller]")]
[RequiresStoreAuthorize]
public class InventoryController : BaseApiController
{
    private readonly IMediator _mediator;

    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public Task<PagedList<InventoryListItemDto>> GetAllProducts([FromQuery] PageRequestQueryModel pageRequest)
        => _mediator.Send(new GetAllInventories(pageRequest), CancellationToken);
}