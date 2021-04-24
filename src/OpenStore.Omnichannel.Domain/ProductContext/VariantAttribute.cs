using System;
using System.Collections.Generic;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class VariantAttribute : Entity<Guid>
    {
        private readonly HashSet<VariantAttributeValue> _values = new();
        private readonly HashSet<Product> _products = new();
        
        public string Name { get; protected set; }
        public virtual IReadOnlyCollection<VariantAttributeValue> Values => _values;
        public virtual IReadOnlyCollection<Product> Products => _products;
        
        public VariantAttribute(string name)
        {
            Name = name;
        }
    }
}