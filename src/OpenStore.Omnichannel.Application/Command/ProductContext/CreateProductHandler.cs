using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class CreateProductHandler : ICommandHandler<CreateProduct, Guid>
{
    private readonly ICrudRepository<ProductMedia> _productMediaRepository;
    private readonly ICrudRepository<Product> _repository;

    public CreateProductHandler(
        ICrudRepository<Product> repository,
        ICrudRepository<ProductMedia> productMediaRepository
    )
    {
        _repository = repository;
        _productMediaRepository = productMediaRepository;
    }

    public async Task<Guid> Handle(CreateProduct command, CancellationToken cancellationToken)
    {
        if (await _repository.Query.AnyAsync(x => x.Handle == command.Model.Handle, cancellationToken))
            throw new DomainException(Msg.Domain.Product.ProductHandleAlreadyExists);

        var product = Product.Create(command, id => _productMediaRepository.Query.Single(x => x.Id == id));
        await _repository.InsertAsync(product, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}