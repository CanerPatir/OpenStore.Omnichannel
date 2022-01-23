using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class ArchiveProductHandler : CommandHandler<ArchiveProduct>
{
    private readonly ICrudRepository<Product> _repository;

    public ArchiveProductHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(ArchiveProduct command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.Id, cancellationToken);
        product.Archive();
        await _repository.SaveChangesAsync(cancellationToken);
    }
}