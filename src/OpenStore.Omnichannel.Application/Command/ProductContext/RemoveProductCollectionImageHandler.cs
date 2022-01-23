using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class RemoveProductCollectionImageHandler : CommandHandler<RemoveProductCollectionImage>
{
    private readonly ICrudRepository<ProductCollection> _repository;
    private readonly IObjectStorageService _objectStorageService;

    public RemoveProductCollectionImageHandler(ICrudRepository<ProductCollection> repository, IObjectStorageService objectStorageService)
    {
        _repository = repository;
        _objectStorageService = objectStorageService;
    }

    protected override async Task Handle(RemoveProductCollectionImage command, CancellationToken cancellationToken)
    {
        var collection = await _repository.GetAsync(command.ProductCollectionId, cancellationToken);
        var host = collection.Media.Host;
        var path = collection.Media.Path;
        collection.RemoveImage();
        await _repository.SaveChangesAsync(cancellationToken);
        await _objectStorageService.Delete(host, path);
    }
}