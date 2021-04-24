using System;
using System.Collections.Generic;
using OpenStore.Domain;

// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable ConvertToAutoProperty
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class Variant : Entity<Guid>
    {
        private readonly HashSet<VariantAttributeValue> _variantAttributeValues = new();

        public Guid ProductId { get; protected set; }
        
        public virtual IReadOnlyCollection<VariantAttributeValue> VariantAttributeValues => _variantAttributeValues;

        public Variant(Guid productId)
        {
            ProductId = productId;
        }

    }
}