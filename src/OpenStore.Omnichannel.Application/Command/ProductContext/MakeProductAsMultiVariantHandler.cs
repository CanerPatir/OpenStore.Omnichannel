using MediatR;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.ProductContext;

namespace OpenStore.Omnichannel.Application.Command.ProductContext;

public class MakeProductAsMultiVariantHandler : IRequestHandler<MakeProductAsMultiVariant, IEnumerable<Guid>>
{
    private readonly ICrudRepository<Product> _repository;
    private readonly ICrudRepository<Variant> _variantRepository;

    public MakeProductAsMultiVariantHandler(ICrudRepository<Product> repository, ICrudRepository<Variant> variantRepository)
    {
        _repository = repository;
        _variantRepository = variantRepository;
    }

    public async Task<IEnumerable<Guid>> Handle(MakeProductAsMultiVariant command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetAsync(command.ProductId, cancellationToken);

        if (!product.HasMultipleVariants)
        {
            _variantRepository.Remove(product.Variants.First());
        }

        var variants = product.MakeMultiVariant(command);
        var idVariants = new List<Variant>();
        foreach (var variant in variants)
        {
            idVariants.Add(await _variantRepository.InsertAsync(variant, cancellationToken));
        }

        await _repository.SaveChangesAsync(cancellationToken);

        return idVariants.Select(x => x.Id);
    }
}