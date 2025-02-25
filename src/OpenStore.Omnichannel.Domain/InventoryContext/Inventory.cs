using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.ProductContext;
using OpenStore.Omnichannel.Shared.Command.InventoryContext;
using OpenStore.Omnichannel.Shared.DomainEvents.InventoryContext;

namespace OpenStore.Omnichannel.Domain.InventoryContext;

public class Inventory : Entity<Guid>, IAuditableEntity
{
    public Guid VariantId { get; protected set; }
    public virtual Variant Variant { get; protected set; }

    public int Quantity { get; protected set; }
    public int AvailableQuantity { get; protected set; }
    public bool ContinueSellingWhenOutOfStock { get; protected set; }

    #region auditable members

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    #endregion

    protected Inventory()
    {
    }

    private Inventory(Guid variantId, int quantity, bool continueSellingWhenOutOfStock)
    {
        // Id = Guid.NewGuid();
        VariantId = variantId;
        Quantity = quantity;
        AvailableQuantity = quantity;
        ContinueSellingWhenOutOfStock = continueSellingWhenOutOfStock;
    }

    public static Inventory Create(Guid variantId, int quantity, bool continueSellingWhenOutOfStock)
    {
        var inventory = new Inventory(variantId, quantity, continueSellingWhenOutOfStock)
        {
            Id = Guid.NewGuid()
        };
        inventory.ApplyChange(new InventoryCreated(inventory.Id, inventory.VariantId, inventory.Quantity, inventory.ContinueSellingWhenOutOfStock));
        return inventory;
    }

    internal static Inventory CreateDefault() => new();

    public void Change(int quantity)
    {
        if (quantity < 0)
        {
            throw new DomainException(Msg.Domain.Inventory.QuantityShouldBeGreaterOrEqualThenZero);
        }

        var diff = Quantity - quantity;
        Quantity = quantity;
        AvailableQuantity = quantity;

        ApplyChange(new InventoryChanged(Id, Quantity, AvailableQuantity, ContinueSellingWhenOutOfStock));
    }

    public void AddQuantity(AddInventoryQuantity command)
    {
        Change(AvailableQuantity + command.Quantity);
    }
    
    public void SetQuantity(SetInventoryQuantity command)
    {
        Change(command.Quantity);
    }

    public void ChangeContinueSellingWhenOutOfStockStatus(bool continueSellingWhenOutOfStock)
    {
        ContinueSellingWhenOutOfStock = continueSellingWhenOutOfStock;

        ApplyChange(new InventoryChanged(Id, Quantity, AvailableQuantity, ContinueSellingWhenOutOfStock));
    }

    public void Decrease(int quantity)
    {
        if (!ContinueSellingWhenOutOfStock && AvailableQuantity < quantity)
        {
            throw new DomainException(Msg.Domain.Inventory.OutOfStock);
        }

        AvailableQuantity -= quantity;

        ApplyChange(new InventoryReserved(Id, Quantity, AvailableQuantity));
    }

    // public void Release(int quantity)
    // {
    //     var newAvailableQty = AvailableQuantity + quantity;
    //     AvailableQuantity = Math.Max(Quantity, newAvailableQty);
    //
    //     ApplyChange(new InventoryReleased(Id, Quantity, AvailableQuantity));
    // }
}