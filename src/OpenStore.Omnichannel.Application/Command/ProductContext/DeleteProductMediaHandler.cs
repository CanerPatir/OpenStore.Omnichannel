using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class DeleteProductMediaHandler : ICommandHandler<DeleteProductMedia>
{
    private readonly IObjectStorageService _objectStorageService;
    private readonly ICrudRepository<ProductMedia> _productMediaRepository;
    private readonly ICrudRepository<Product> _repository;

    public DeleteProductMediaHandler(
        ICrudRepository<Product> repository,
        ICrudRepository<ProductMedia> productMediaRepository,
        IObjectStorageService objectStorageService
    )
    {
        _repository = repository;
        _productMediaRepository = productMediaRepository;
        _objectStorageService = objectStorageService;
    }

    public async Task Handle(DeleteProductMedia command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.Id, cancellationToken);
        product.DeleteMedia(command);

        var productMedia = await _productMediaRepository.GetAsync(command.ProductMediaId, cancellationToken);
        await _productMediaRepository.Remove(productMedia);

        await _productMediaRepository.SaveChangesAsync(cancellationToken);

#pragma warning disable 4014
        _objectStorageService.Delete(productMedia.Host, productMedia.Path);
#pragma warning restore 4014
    }
}