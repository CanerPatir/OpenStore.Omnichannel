using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class ChangeVariantMediaHandler : CommandHandler<ChangeVariantMedia>
{
    private readonly ICrudRepository<Product> _repository;

    public ChangeVariantMediaHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(ChangeVariantMedia command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetRequired(command.ProductId, cancellationToken);
        product.ChangeVariantMedia(command);
        await _repository.SaveChangesAsync(cancellationToken);
        
    }
}