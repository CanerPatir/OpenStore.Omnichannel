using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OpenStore.Infrastructure.Web;
using OpenStore.Omnichannel.Application.Command;
using OpenStore.Omnichannel.Infrastructure.Authentication;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Media;

namespace OpenStore.Omnichannel.Api.Store
{
    [Route("api/[controller]")]
    [RequiresStoreAuthorize]
    public class MediaController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public MediaController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        [HttpPost]
        public Task<IEnumerable<MediaDto>> Upload([FromBody] IEnumerable<FileUploadDto> model)
        {
            var environment = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

            async Task<(string host, string path)> FileDelegate(string fileName, byte[] content)
            {
                const string rootDir = "content";
                var dir = Path.Combine(environment.WebRootPath, rootDir);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                await using var fs = System.IO.File.Create(Path.Combine(dir, fileName));
                await fs.WriteAsync(content.AsMemory(0, content.Length));

                return ($"{Request.Scheme}://{Request.Host}", $"/{rootDir}/{fileName}");
            }

            return _mediator.Send(new CreateMedia(model, FileDelegate), CancellationToken);
        }
    }
}