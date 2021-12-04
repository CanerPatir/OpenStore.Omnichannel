using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.CheckoutContext;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Query.Storefront;
using OpenStore.Omnichannel.Shared.Query.Storefront.Result;

namespace OpenStore.Omnichannel.ReadModel.Sql.Storefront;

public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartResult>
{
    private readonly ICrudRepository<ShoppingCart> _repository;
    private readonly ICrudRepository<Variant> _variantRepository;

    public GetShoppingCartQueryHandler(ICrudRepository<ShoppingCart> repository, ICrudRepository<Variant> variantRepository)
    {
        _repository = repository;
        _variantRepository = variantRepository;
    }

    public async Task<ShoppingCartResult> Handle(GetShoppingCartQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.Query.AsNoTracking().SingleOrDefaultAsync(x => x.Id == query.CartId, cancellationToken);

        var getVariantsTasks = shoppingCart
            .Items
            .Select(
                async x => await _variantRepository.Query
                    .Include(v => v.Inventory)
                    .Include(v => v.Product)
                        .ThenInclude(p => p.Medias)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(v => v.Id == x.VariantId, cancellationToken)
            );

        var variants = await Task.WhenAll(getVariantsTasks);

        return new ShoppingCartResult(
            shoppingCart.Id,
            shoppingCart.IsAuthenticated,
            shoppingCart.Items.Select(x =>
            {
                var variant = variants.SingleOrDefault(v => v.Id == x.VariantId);

                if (variant is null || variant.Inventory.AvailableQuantity < x.Quantity)
                {
                    return null;
                }

                return new ShoppingCartItemDto(
                    x.Id,
                    x.VariantId,
                    x.Quantity,
                    variant.Inventory.AvailableQuantity,
                    variant.Product.Id,
                    variant.Product.Handle,
                    variant.Product.Title,
                    variant.Title,
                    variant.Product.GetVariantMediaOrDefault(variant.Id)?.Url,
                    variant.Price
                );
            }).Where(x => x is not null).ToList()
        );
    }
}