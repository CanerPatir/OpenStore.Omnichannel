namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductPricingDto
    {
        public decimal Price { get; set; } = 0;
        public decimal? CompareAtPrice { get; set; }
        public decimal? Cost { get; set; }
    }
}