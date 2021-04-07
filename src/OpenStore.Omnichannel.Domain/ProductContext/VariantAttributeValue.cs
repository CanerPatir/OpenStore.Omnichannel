using System;
using OpenStore.Omnichannel.Domain.LookupContext;
using Attribute = OpenStore.Omnichannel.Domain.LookupContext.Attribute;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class VariantAttributeValue : IEquatable<VariantAttributeValue>
    {
        public Guid VariantId { get; protected set; }
        public virtual Variant Variant { get; protected set; }

        public Guid AttributeId { get; protected set; }
        public virtual Attribute Attribute { get; protected set; }

        public Guid? AttributeValueId { get; protected set; }
        public virtual AttributeValue AttributeValue { get; protected set; }

        public string CustomValue { get; protected set; }

        protected VariantAttributeValue()
        {
        }

        public VariantAttributeValue(Variant variant, Attribute attribute, AttributeValue attributeValue)
        {
            Variant = variant;
            VariantId = variant.Id;
            Attribute = attribute;
            AttributeId = attribute.Id;
            AttributeValue = attributeValue;
            AttributeValueId = attributeValue.Id;
        }

        public VariantAttributeValue(Variant variant, Attribute attribute, string customValue)
        {
            Variant = variant;
            VariantId = variant.Id;
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

        public bool Equals(VariantAttributeValue other)
            => other != null && (Equals(VariantId, other.VariantId) && Equals(AttributeId, other.AttributeId) && Equals(AttributeValueId, other.AttributeValueId));

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((VariantAttributeValue) obj);
        }

        public override int GetHashCode() => HashCode.Combine(VariantId, Attribute, AttributeValue);

        #endregion
    }
}