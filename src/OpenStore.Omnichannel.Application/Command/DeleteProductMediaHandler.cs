using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command
{
    public class DeleteProductMediaHandler : IRequestHandler<DeleteProductMedia>
    {
        private readonly ICrudRepository<Product> _repository;
        private readonly ICrudRepository<ProductMedia> _productMediaRepository;
        private readonly IObjectStorageService _objectStorageService;

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

        public async Task<Unit> Handle(DeleteProductMedia command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(command.Id, cancellationToken);
            product.DeleteMedia(command);

            var productMedia = await _productMediaRepository.GetAsync(command.ProductMediaId, cancellationToken);
            _productMediaRepository.Remove(productMedia);
            
            await _productMediaRepository.SaveChangesAsync(cancellationToken);

#pragma warning disable 4014
            _objectStorageService.Delete(productMedia.Host, productMedia.Path);
#pragma warning restore 4014
            
            return Unit.Value;
        }
    }
}