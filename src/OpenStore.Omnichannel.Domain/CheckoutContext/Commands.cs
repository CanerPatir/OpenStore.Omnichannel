using MediatR;

namespace OpenStore.Omnichannel.Domain.CheckoutContext;

public record CreateShoppingCart(Guid? UserId) : IRequest<Guid>;

public record AddItemToCart(Guid CartId, Guid VariantId, int Quantity) : IRequest<Guid>;

public record RemoveItemFromCart(Guid CartId, Guid CartItemId) : IRequest;

public record ChangeItemQuantityOfCart(Guid CartId, Guid CartItemId, int Quantity) : IRequest;

public record BindCartToUser(Guid CartId, Guid UserId) : IRequest;