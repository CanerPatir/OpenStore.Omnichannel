using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UnArchiveProductHandler : CommandHandler<UnArchiveProduct>
{
    private readonly ICrudRepository<Product> _repository;

    public UnArchiveProductHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UnArchiveProduct command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.Id, cancellationToken);
        product.UnArchive();
        await _repository.SaveChangesAsync(cancellationToken);
    }
}