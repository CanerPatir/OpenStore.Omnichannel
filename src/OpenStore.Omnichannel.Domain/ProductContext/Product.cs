using System;
using System.Collections.Generic;
using System.Linq;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.LookupContext;

// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class Product : AggregateRoot<Guid>, IAuditableEntity, ISoftDeleteEntity
    {
        private readonly HashSet<Variant> _variants = new();
        private readonly HashSet<ProductOption> _options = new();
        private readonly HashSet<ProductMedia> _medias = new();

        public string Handle { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public bool HasMultipleVariants { get; protected set; }
        public ProductStatus Status { get; protected set; }

        public string MetaTitle { get; protected set; }
        public string MetaDescription { get; protected set; }
        public string Tags { get; protected set; }
        
        public decimal? Weight { get; protected set; }
        public string WeightUnit { get; protected set; }
        public string HsCode { get; protected set; }
        public bool IsPhysicalProduct { get; protected set; }

        public Guid? BrandId { get; protected set; }
        public virtual Brand Brand { get; protected set; }

        public virtual IReadOnlyCollection<Variant> Variants => _variants;
        public virtual IReadOnlyCollection<ProductOption> Options => _options;
        public virtual IReadOnlyCollection<ProductMedia> Medias => _medias;

        #region auditable members

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        #endregion

        #region soft delete members

        public bool SoftDeleted { get; set; }

        #endregion

        protected Product()
        {
        }
        
        protected Product(string handle, string title, bool hasMultipleVariants, IEnumerable<ProductOption> options)
        {
            Handle = handle;
            Title = title;
            HasMultipleVariants = hasMultipleVariants;
            Status = ProductStatus.Draft;

            if (HasMultipleVariants)
            {
                if (options is null || !options.Any())
                {
                    throw new DomainException(Msg.Domain.MultipleVariantProductMustHasOptions);
                }
                _options = HasMultipleVariants ? options.ToHashSet() : new HashSet<ProductOption>();
            }
            else
            {
                _options = new HashSet<ProductOption>();
            }
        }

    }
}