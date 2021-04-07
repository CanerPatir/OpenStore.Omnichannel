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
        public Guid ProductId { get; protected set; }

        public string Sku { get; protected set; }

        public int Order { get; protected set; }

        private readonly HashSet<VariantAttributeValue> _attributeValues = new();
        public virtual IReadOnlyCollection<VariantAttributeValue> AttributeValues => _attributeValues;

        public Variant(Guid productId, HashSet<VariantAttributeValue> attributeValues)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            _attributeValues = attributeValues;
        }

        public void SetSku(string sku)
        {
            Sku = sku;
        }
    }
}