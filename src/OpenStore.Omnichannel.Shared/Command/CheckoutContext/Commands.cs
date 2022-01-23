namespace OpenStore.Omnichannel.Shared.Command.CheckoutContext;

public record CreateShoppingCart(Guid? UserId) : ICommand<Guid>;

public record AddItemToCart(Guid CartId, Guid VariantId, int Quantity) : ICommand<Guid>;

public record RemoveItemFromCart(Guid CartId, Guid CartItemId) : ICommand;

public record ChangeItemQuantityOfCart(Guid CartId, Guid CartItemId, int Quantity) : ICommand;

public record BindCartToUser(Guid CartId, Guid UserId) : ICommand;