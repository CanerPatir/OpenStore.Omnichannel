using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Domain.StoreContext;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext;
using OpenStore.Omnichannel.Shared.Query.Management.StoreContext.Result;

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
    public Task<StorePreferencesQueryResult> GetStorePreferences() => _mediator.Send(new GetStorePreferencesQuery(), CancellationToken);

    [HttpPut("preferences")]
    public Task UpdateStorePreferences(StorePreferencesQueryResult model) => _mediator.Send(new UpdateStorePreferences(model), CancellationToken);
}