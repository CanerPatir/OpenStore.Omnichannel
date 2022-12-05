using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;
using OpenStore.Omnichannel.Shared.Dto.Management.Product;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class ChangeProductCollectionImageHandler : ICommandHandler<ChangeProductCollectionImage, ProductCollectionMediaDto>
{
    private readonly IObjectStorageService _objectStorageService;
    private readonly ICrudRepository<ProductCollection> _repository;

    public ChangeProductCollectionImageHandler(ICrudRepository<ProductCollection> repository, IObjectStorageService objectStorageService)
    {
        _repository = repository;
        _objectStorageService = objectStorageService;
    }

    public async Task<ProductCollectionMediaDto> Handle(ChangeProductCollectionImage command, CancellationToken cancellationToken)
    {
        var (productCollectionId, (fileName, type, size, position, fileContent)) = command;
        var productCollection = await _repository.GetAsync(productCollectionId, cancellationToken);
        var (host, path) = await _objectStorageService.Write(FileNameStrategy(fileName), fileContent);

        var dto = productCollection.ChangeImage(command, host, path);
        await _repository.SaveChangesAsync(cancellationToken);
        return dto;
    }

    private static string FileNameStrategy(string fileName)
    {
        return $"{Path.GetFileNameWithoutExtension(fileName)}_{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{Path.GetExtension(fileName)}";
    }
}