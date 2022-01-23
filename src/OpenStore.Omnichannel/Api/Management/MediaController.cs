using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Api.Management;

[Route("api/[controller]")]
[RequiresStoreAuthorize]
public class MediaController : BaseApiController
{
    private readonly IMediator _mediator;

    public MediaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("product")]
    public async Task<IEnumerable<ProductMediaDto>> UploadProductMedia(IEnumerable<FileUploadDto> model) 
        => await _mediator.Send(new CreateProductMedia(model), CancellationToken);
}