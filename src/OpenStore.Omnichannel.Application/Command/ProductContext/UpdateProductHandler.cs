using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductHandler : CommandHandler<UpdateProduct>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(UpdateProduct command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId, cancellationToken);
        if (product.Handle != command.Model.Handle)
        {
            if (await _repository.Query.AnyAsync(x => x.Handle == command.Model.Handle && x.Id != command.ProductId, cancellationToken))
            {
                throw new DomainException(Msg.Domain.Product.ProductHandleAlreadyExists);
            }
        }

        product.UpdatedMasterData(command);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}