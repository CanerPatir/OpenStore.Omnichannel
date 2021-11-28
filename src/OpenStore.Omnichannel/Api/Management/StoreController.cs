using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.StoreContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto.Management.Store;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext;

namespace OpenStore.Omnichannel.Api.Management;

[Route("api/[controller]")]
[RequiresStoreAuthorize]
public class StoreController : BaseApiController
{
    private readonly IMediator _mediator;

    public StoreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("preferences")]
    public Task<StorePreferencesDto> GetStorePreferences() => _mediator.Send(new GetStorePreferences(), CancellationToken);

    [HttpPut("preferences")]
    public Task UpdateStorePreferences(StorePreferencesDto model) => _mediator.Send(new UpdateStorePreferences(model), CancellationToken);
}