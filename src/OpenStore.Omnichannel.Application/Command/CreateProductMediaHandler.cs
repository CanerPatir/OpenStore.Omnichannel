using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Application.Command
{
    public class CreateProductMediaHandler : IRequestHandler<CreateProductMedia, IEnumerable<ProductMediaDto>>
    {
        private readonly ICrudRepository<ProductMedia> _repository;
        private readonly IOpenStoreObjectMapper _mapper;
        private readonly IObjectStorageService _objectStorageService;

        public CreateProductMediaHandler(ICrudRepository<ProductMedia> repository,
            IOpenStoreObjectMapper mapper,
            IObjectStorageService objectStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _objectStorageService = objectStorageService;
        }

        public async Task<IEnumerable<ProductMediaDto>> Handle(CreateProductMedia request, CancellationToken cancellationToken)
        {
            var mediasTasks = request.Uploads.Select(async x =>
            {
                var (host, path) = await _objectStorageService.Write(FileNameStrategy(x.FileName), x.FileContent);
                return ProductMedia.Create(host, path, x.Type, Path.GetExtension(x.FileName), x.FileName, x.Position, x.Size, x.FileName);
            });

            var medias = await Task.WhenAll(mediasTasks);

            foreach (var media in medias)
            {
                await _repository.InsertAsync(media, cancellationToken);
            }

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.MapAll<ProductMediaDto>(medias);
        }

        private static string FileNameStrategy(string fileName) =>
            $"{Path.GetFileNameWithoutExtension(fileName)}_{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileName)}";
    }
}