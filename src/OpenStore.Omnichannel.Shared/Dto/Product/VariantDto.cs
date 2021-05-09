using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class VariantDto
    {
        public Guid? Id { get; set; }

        // Pricing
        public decimal Price { get; set; } = 0;
        public decimal? CompareAtPrice { get; set; }
        public decimal? Cost { get; set; }
        public bool CalculateTaxAdditionally { get; set; }

        // Inventory
        public string Sku { get;  set; }
        public string Barcode { get;  set; }
        public bool TrackQuantity { get; set; } = true;
        public bool ContinueSellingWhenOutOfStock { get; set; }
        public int Quantity { get; set; }

        // option
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        
        [NotMapped] public bool Selected { get; set; }
    }
}