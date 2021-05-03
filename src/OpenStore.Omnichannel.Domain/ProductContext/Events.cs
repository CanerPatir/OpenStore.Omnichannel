using System;
using System.Collections.Generic;
using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public abstract record DomainEventBase(Guid EntityId) : DomainEvent(EntityId.ToString());

    public record ProductMediaCreated(
        Guid ProductMediaId,
        string Host,
        string Path,
        string Type,
        string Extension,
        string Filename,
        string Title,
        int Position,
        long? Size) : DomainEventBase(ProductMediaId);

    public record ProductCreated(
        Guid ProductId, string Handle, string Title,
        string Description, bool HasMultipleVariants, ProductStatus Status,
        IEnumerable<ProductOptionDto> Options, string MetaTitle, string MetaDescription, string Tags,
        bool IsPhysicalProduct, decimal? Weight, string WeightUnit, string HsCode
    ) : DomainEventBase(ProductId)
    {
        public static ProductCreated From(Product product, IEnumerable<ProductOptionDto> options)
            => new(
                product.Id,
                product.Handle,
                product.Title,
                product.Description,
                product.HasMultipleVariants,
                product.Status,
                options,
                product.MetaTitle,
                product.MetaDescription,
                product.Tags,
                product.IsPhysicalProduct,
                product.Weight,
                product.WeightUnit,
                product.HsCode
            );
    }

    public record VariantAddedToProduct(Guid ProductId, VariantDto Variant) : DomainEventBase(ProductId);
    
    public record MediaAssignedToProduct(Guid ProductId, ProductMediaDto ProductMedia) : DomainEventBase(ProductId);
}