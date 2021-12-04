using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class UpdateProductHandler : IRequestHandler<UpdateProduct>
{
    private readonly ICrudRepository<Product> _repository;

    public UpdateProductHandler(ICrudRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProduct command, CancellationToken cancellationToken)
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
        return Unit.Value;
    }
}