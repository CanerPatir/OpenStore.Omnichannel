using MediatR;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class CreateProductMediaHandler : IRequestHandler<CreateProductMedia, IEnumerable<(ProductMediaDto, ProductMedia)>>
{
    private readonly ICrudRepository<ProductMedia> _repository;
    private readonly IOpenStoreObjectMapper _mapper;
    private readonly IObjectStorageService _objectStorageService;

    public CreateProductMediaHandler(
        ICrudRepository<ProductMedia> repository,
        IOpenStoreObjectMapper mapper,
        IObjectStorageService objectStorageService
    )
    {
        _repository = repository;
        _mapper = mapper;
        _objectStorageService = objectStorageService;
    }

    public async Task<IEnumerable<(ProductMediaDto, ProductMedia)>> Handle(CreateProductMedia command, CancellationToken cancellationToken)
    {
        var mediasTasks = command.Uploads.Select(async x =>
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

        return medias.Select(x => (_mapper.Map<ProductMediaDto>(x), x));
    }

    private static string FileNameStrategy(string fileName) =>
        $"{Path.GetFileNameWithoutExtension(fileName)}_{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileName)}";
}