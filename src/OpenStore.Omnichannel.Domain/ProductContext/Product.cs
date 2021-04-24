using System;
using System.Collections.Generic;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.LookupContext;
using OpenStore.Omnichannel.Domain.MediaContext;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class Product : Entity<Guid>
    {
        private readonly HashSet<Variant> _variants = new();
        private readonly HashSet<VariantAttribute> _variantAttributes = new();

        public string Handle { get; protected set; }   
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public bool HasMultipleVariants { get; protected set; }
        public ProductStatus Status { get; protected set; }

        public string MetaTitle { get; protected set; }
        public string MetaDescription { get; protected set; }
        public string Tags { get; protected set; }
        
        public decimal? Weight { get; protected set; }
        public string HsCode { get; protected set; }
        public bool IsPhysicalProduct { get; protected set; }

        public Guid? BrandId { get; protected set; }
        public virtual Brand Brand { get; protected set; }
        
        public virtual IReadOnlyCollection<Variant> Variants => _variants;
        public virtual IReadOnlyCollection<VariantAttribute> VariantAttributes => _variantAttributes;

        protected Product(string handle, string title, bool hasMultipleVariants)
        {
            Handle = handle;
            Title = title;
            HasMultipleVariants = hasMultipleVariants;
            Status = ProductStatus.Draft;
        }
        
        
    }
}