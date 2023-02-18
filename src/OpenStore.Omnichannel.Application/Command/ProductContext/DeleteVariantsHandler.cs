using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class DeleteVariantsHandler : ICommandHandler<DeleteVariants>
{
    private readonly ICrudRepository<Product> _repository;
    private readonly ICrudRepository<Variant> _variantRepository;

    public DeleteVariantsHandler(ICrudRepository<Product> repository, ICrudRepository<Variant> variantRepository)
    {
        _repository = repository;
        _variantRepository = variantRepository;
    }

    public async Task Handle(DeleteVariants command, CancellationToken cancellationToken)
    {
        var (productId, variantIds) = command;
        var product = await _repository.Query.Include(x => x.Variants).SingleOrDefaultAsync(x => x.Id == productId, cancellationToken);
        var deleteVariants = product.DeleteVariants(command);
        foreach (var deleteVariant in deleteVariants) await _variantRepository.Remove(deleteVariant);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}