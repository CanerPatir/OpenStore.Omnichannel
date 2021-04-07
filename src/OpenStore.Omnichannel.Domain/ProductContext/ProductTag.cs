using System;
using OpenStore.Omnichannel.Domain.LookupContext;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class ProductTag : IEquatable<ProductTag>
    {
        public Guid ProductId { get; protected set; }
        public virtual Product Product { get; protected set; }

        public Guid TagId { get; protected set; }
        public virtual Tag Tag { get; protected set; }

        public ProductTag(Guid productId, Guid tagId)
        {
            ProductId = productId;
            TagId = tagId;
        }

        #region equality

        public bool Equals(ProductTag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductId == other.ProductId && TagId == other.TagId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((ProductTag) obj);
        }

        public override int GetHashCode() => HashCode.Combine(ProductId, TagId);

        #endregion
    }
}