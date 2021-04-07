using System;
using OpenStore.Omnichannel.Domain.LookupContext;
using Attribute = OpenStore.Omnichannel.Domain.LookupContext.Attribute;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public abstract class BaseProductAttributeValue : IEquatable<BaseProductAttributeValue>
    {
        public Guid ProductId { get; protected set; }
        public virtual Product Product { get; protected set; }

        public Guid AttributeId { get; protected set; }
        public virtual Attribute Attribute { get; protected set; }

        public Guid? AttributeValueId { get; protected set; }
        public virtual AttributeValue AttributeValue { get; protected set; }

        public string CustomValue { get; protected set; }

        protected BaseProductAttributeValue(Product product, Attribute attribute, AttributeValue attributeValue)
        {
            Product = product;
            ProductId = product.Id;
            Attribute = attribute;
            AttributeId = attribute.Id;
            AttributeValue = attributeValue;
            AttributeValueId = attributeValue.Id;
        }

        protected BaseProductAttributeValue(Product product, Attribute attribute, string customValue)
        {
            Product = product;
            ProductId = product.Id;
            Attribute = attribute;
            AttributeId = attribute.Id;
            CustomValue = customValue;
        }

        public void ChangeValue(AttributeValue attributeValue)
        {
            AttributeValue = attributeValue;
        }
        
        public void ChangeValue(string customValue)
        {
            CustomValue = customValue;
        }
        
        #region equality

        public bool Equals(BaseProductAttributeValue other)
            => other != null && (Equals(ProductId, other.ProductId) && Equals(AttributeId, other.AttributeId) && Equals(AttributeValueId, other.AttributeValueId));

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType().IsInstanceOfType(this)  && Equals((BaseProductAttributeValue) obj);
        }

        public override int GetHashCode() => HashCode.Combine(ProductId, Attribute, AttributeValue);

        #endregion
    }
}