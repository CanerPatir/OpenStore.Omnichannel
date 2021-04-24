using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.MediaContext;
using OpenStore.Omnichannel.Shared.Dto;
using OpenStore.Omnichannel.Shared.Dto.Media;

namespace OpenStore.Omnichannel.Application.Command
{
    public record CreateMedia(IEnumerable<FileUploadDto> Uploads, Func<string, byte[], Task<(string host, string path)>> FileDelegate) : IRequest<IEnumerable<MediaDto>>;

    public class CreateMediaHandler : IRequestHandler<CreateMedia, IEnumerable<MediaDto>>
    {
        private readonly ICrudRepository<Media> _repository;
        private readonly IOpenStoreObjectMapper _mapper;

        public CreateMediaHandler(ICrudRepository<Media> repository, IOpenStoreObjectMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MediaDto>> Handle(CreateMedia request, CancellationToken cancellationToken)
        {
            var mediasTasks = request.Uploads.Select(async x =>
            {
                var (host, path) = await request.FileDelegate(FileNameStrategy(x.FileName), x.FileContent);
                return new Media(host, path, x.Type, Path.GetExtension(x.FileName), x.FileName)
                {
                    Position = x.Position,
                    Size = x.Size,
                    Title = x.FileName
                };
            });

            var medias = await Task.WhenAll(mediasTasks);
            
            foreach (var media in medias)
            {
                await _repository.InsertAsync(media, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.MapAll<MediaDto>(medias);
        }

        private static string FileNameStrategy(string fileName) =>
            $"{Path.GetFileNameWithoutExtension(fileName)}_{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileName)}";
    }
}