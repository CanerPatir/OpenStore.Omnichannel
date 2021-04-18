namespace OpenStore.Omnichannel.Shared.Dto.Product
{
    public class ProductShippingInfoDto
    {
        public bool IsPhysicalProduct { get; set; }
        public decimal Weight { get; set; }
        public string HsCode { get; set; }
    }
}