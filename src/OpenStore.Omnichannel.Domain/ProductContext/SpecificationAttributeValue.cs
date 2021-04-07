using OpenStore.Omnichannel.Domain.LookupContext;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class SpecificationAttributeValue : BaseProductAttributeValue
    {
        public SpecificationAttributeValue(Product product, Attribute attribute, AttributeValue attributeValue) : base(product, attribute, attributeValue)
        {
        }

        public SpecificationAttributeValue(Product product, Attribute attribute, string customValue) : base(product, attribute, customValue)
        {
        }
    }
}