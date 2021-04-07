using System;
using OpenStore.Domain;

namespace OpenStore.Omnichannel.Domain.ProductContext.Event
{
    // todo: enrich available events
    public abstract record ProductEvent(Guid ProductId) : DomainEvent(ProductId.ToString());

    public record ProductCreated(Guid ProductId, Guid CategoryId, Guid BrandId, string Name, string Description) : ProductEvent(
        ProductId);

    public record VariantCreated(Guid ProductId, Guid VariantId, string VariantSku) : ProductEvent(ProductId);

    public record ProductGroupAttributeSet(Guid ProductId, Guid AttributeId, Guid? AttributeValueId, string CustomValue) : ProductEvent(ProductId);

    public record ProductSpecificationAttributeSet(Guid ProductId, Guid AttributeId, Guid? AttributeValueId, string CustomValue) : ProductEvent(ProductId);

    public record ProductSpecificationAttributeRemoved(Guid ProductId, Guid AttributeId) : ProductEvent(ProductId);

    public record MediaAdded(Guid ProductId, string Path) : ProductEvent(ProductId);

    public record TagAdded(Guid ProductId, Guid TagId, string TagName) : ProductEvent(ProductId);

    public record TagRemoved(Guid ProductId, Guid TagId) : ProductEvent(ProductId);
}