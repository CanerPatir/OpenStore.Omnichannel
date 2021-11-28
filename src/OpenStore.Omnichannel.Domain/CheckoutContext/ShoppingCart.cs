using OpenStore.Domain;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenStore.Omnichannel.Domain.CheckoutContext;

public class ShoppingCart : AggregateRoot<Guid>, IAuditableEntity
{
    private readonly HashSet<ShoppingCartItem> _items = new();

    protected ShoppingCart()
    {
        Id = Guid.NewGuid();
    }

    public Guid? UserId { get; protected set; }
    public bool IsAuthenticated { get; protected set; }

    public virtual IReadOnlyCollection<ShoppingCartItem> Items => _items;

    public static ShoppingCart Create(CreateShoppingCart command) =>
        new()
        {
            UserId = command.UserId,
            IsAuthenticated = command.UserId.HasValue
        };

    public Guid AddItem(AddItemToCart command)
    {
        var (_, variantId, quantity) = command;

        if (_items.Any(x => x.VariantId == variantId)) throw new DomainException(Msg.Domain.Checkout.ShoppingCartAlreadyContainsTheGivenVariant);

        var item = ShoppingCartItem.Create(variantId, quantity);
        _items.Add(item);
        return item.Id;
    }

    public int RemoveItem(RemoveItemFromCart command)
    {
        return _items.RemoveWhere(x => x.Id == command.CartItemId);
    }

    public void ChangeItemQuantity(ChangeItemQuantityOfCart command)
    {
        var (_, cartItemId, quantity) = command;
        var item = _items.SingleOrDefault(x => x.Id == cartItemId);

        if (item == null)
        {
            throw new DomainException(Msg.Domain.Checkout.ShoppingCartItemNotFound);
        }

        item.ChangeQuantity(quantity);
    }

    public void BindToUser(BindCartToUser command)
    {
        var (_, userId) = command;
        UserId = userId;
        IsAuthenticated = true;
    }

    #region auditable members

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    #endregion
}