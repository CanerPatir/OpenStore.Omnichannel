using System;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.InventoryContext;
using OpenStore.Omnichannel.Shared.Dto.Product;

// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable ConvertToAutoProperty
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class Variant : AuditableEntity
    {
        public Guid ProductId { get; protected set; }

        // option
        public string Option1 { get; protected set; }
        public string Option2 { get; protected set; }
        public string Option3 { get; protected set; }

        // Pricing
        public decimal Price { get; protected set; } = 0;
        public decimal? CompareAtPrice { get; protected set; }
        public decimal? Cost { get; protected set; }
        public bool CalculateTaxAdditionally { get; set; }

        // Inventory

        public string Sku { get; protected set; }
        public string Barcode { get; protected set; }
        public bool TrackQuantity { get; protected set; }
        public bool ContinueSellingWhenOutOfStock { get; protected set; }

        public virtual Inventory Inventory { get; protected set; }

        protected Variant()
        {
        }

        private Variant(Guid productId,
            string option1, string option2, string option3,
            decimal price, decimal? compareAtPrice, decimal? cost, bool calculateTaxAdditionally,
            int quantity, string sku, string barcode, bool trackQuantity, bool continueSellingWhenOutOfStock)
        {
            // Id = Guid.NewGuid();
            ProductId = productId;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;

            Price = price;
            CompareAtPrice = compareAtPrice;
            Cost = cost;
            CalculateTaxAdditionally = calculateTaxAdditionally;

            Sku = sku;
            Barcode = barcode;
            TrackQuantity = trackQuantity;
            if (TrackQuantity)
            {
                Inventory = Inventory.Create(Id, quantity, continueSellingWhenOutOfStock);
            }
        }

        internal static Variant Create(Guid productId, VariantDto variantDto)
        {
            return new(
                productId,
                variantDto.Option1,
                variantDto.Option2,
                variantDto.Option3,
                variantDto.Price,
                variantDto.CompareAtPrice,
                variantDto.Cost,
                variantDto.CalculateTaxAdditionally,
                variantDto.Quantity,
                variantDto.Sku,
                variantDto.Barcode,
                variantDto.TrackQuantity,
                variantDto.ContinueSellingWhenOutOfStock
            )
            {
                Id = Guid.NewGuid()
            };
        }

        internal static Variant CreateDefaultVariant(Guid productId) => new()
        {
            ProductId = productId,
            TrackQuantity = true
        };

        public void UpdateQuantity(int quantity)
        {
            if (!TrackQuantity)
            {
                throw new DomainException(Msg.Domain.Product.VariantStockIsNotTracking);
            }

            Inventory.Change(quantity);
        }

        public void UpdatePrice(decimal price, decimal? compareAtPrice, decimal? cost)
        {
            Price = price;
            CompareAtPrice = compareAtPrice;
            Cost = cost;
        }

        public void UpdateBarcode(string barcode)
        {
            Barcode = barcode;
        }

        public void UpdateSku(string sku)
        {
            Sku = sku;
        }
    }
}