using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class VariantAttributeValue : Entity<Guid>
    {
        public Guid VariantAttributeId { get; protected set; }
        public virtual VariantAttribute VariantAttribute { get; protected set; }
        
        public string Value { get; protected set; }

        public VariantAttributeValue(Guid variantAttributeId, string value)
        {
            VariantAttributeId = variantAttributeId;
            Value = value;
        }
    }
}