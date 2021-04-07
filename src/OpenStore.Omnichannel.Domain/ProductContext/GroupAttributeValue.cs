
using OpenStore.Omnichannel.Domain.LookupContext;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class GroupAttributeValue : BaseProductAttributeValue
    {

        public GroupAttributeValue(Product product, Attribute attribute, AttributeValue attributeValue) : base(product, attribute, attributeValue)
        {
        }

        public GroupAttributeValue(Product product, Attribute attribute, string customValue) : base(product, attribute, customValue)
        {
        }
    }
}