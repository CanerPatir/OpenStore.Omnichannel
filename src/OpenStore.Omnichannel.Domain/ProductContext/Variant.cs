using System;
using System.Collections.Generic;

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
        public int Quantity { get; protected set; }
        
        // option
        public string Option1 { get; protected set; }
        public string Option2 { get; protected set; }
        public string Option3 { get; protected set; }
        
        public Variant(Guid productId, string option1, string option2, string option3, bool trackQuantity, bool continueSellingWhenOutOfStock)
        {
            ProductId = productId;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            TrackQuantity = trackQuantity;
            ContinueSellingWhenOutOfStock = continueSellingWhenOutOfStock;
        }
    }
}