using System.ComponentModel.DataAnnotations.Schema;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.LookupContext;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext;

public partial class Product : AggregateRoot<Guid>, IAuditableEntity, ISoftDeleteEntity
{
    private List<ProductOption> _options = new();
    private readonly HashSet<Variant> _variants = new();
    private readonly HashSet<ProductMedia> _medias = new();

    public string Handle { get; protected set; }
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public bool HasMultipleVariants { get; protected set; }
    public ProductStatus Status { get; protected set; }

    public string MetaTitle { get; protected set; }
    public string MetaDescription { get; protected set; }
    public string Tags { get; protected set; }

    public bool IsPhysicalProduct { get; protected set; }
    public decimal? Weight { get; protected set; }
    public string WeightUnit { get; protected set; }
    public string HsCode { get; protected set; }

    public Guid? BrandId { get; protected set; }
    public virtual Brand Brand { get; protected set; }

    public virtual IReadOnlyCollection<ProductOption> Options => _options;
    public virtual IReadOnlyCollection<Variant> Variants => _variants;
    public virtual IReadOnlyCollection<ProductMedia> Medias => _medias;

    [NotMapped] public ProductMedia FirstMedia => Medias.OrderBy(x => x.Position).FirstOrDefault();

    #region auditable members

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    #endregion

    #region soft delete members

    public bool SoftDeleted { get; set; }

    #endregion

    [NotMapped] public bool IsSingleVariant => !HasMultipleVariants;

    protected Product()
    {
    }

    public IEnumerable<ProductMedia> GetVariantMedias(Guid variantId) => Medias.Where(x => x.VariantIds.Contains(variantId));
    public ProductMedia GetVariantMedia(Guid variantId) => GetVariantMedias(variantId).FirstOrDefault();
    public ProductMedia GetVariantMediaOrDefault(Guid variantId) => GetVariantMedias(variantId).FirstOrDefault() ?? FirstMedia;
}