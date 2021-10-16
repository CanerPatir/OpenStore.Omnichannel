using System;
using System.Collections.Generic;
using OpenStore.Omnichannel.Shared.Dto.Product;

// ReSharper disable once CheckNamespace
namespace OpenStore.Omnichannel.Domain.ProductContext;

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
) : DomainEventBase(ProductId);

public record ProductMasterDataUpdated(
    Guid ProductId,
    string Handle, string Title, string Description, ProductStatus Status,
    decimal? Weight, string WeightUnit, string HsCode, bool IsPhysicalProduct,
    string MetaTitle, string MetaDescription, string Tags) : DomainEventBase(ProductId);

public record VariantMasterDataUpdated(Guid ProductId, Guid VariantId,
    decimal Price, decimal? CompareAtPrice, decimal? Cost, bool CalculateTaxAdditionally,
    string Barcode, string Sku, bool TrackQuantity, bool ContinueSellingWhenOutOfStock) : DomainEventBase(ProductId);

public record VariantOptionsUpdated(Guid ProductId, Guid VariantId, string Option1, string Option2, string Option3) : DomainEventBase(ProductId);

public record VariantAddedToProduct(Guid ProductId, VariantDto Variant) : DomainEventBase(ProductId);

public record MediaAssignedToProduct(Guid ProductId, ProductMediaDto ProductMedia) : DomainEventBase(ProductId);

public record ProductMediaUpdated(Guid ProductId, ProductMediaDto ProductMedia) : DomainEventBase(ProductId);

public record ProductMediaUpdate(Guid ProductId, ProductMediaDto ProductMedia) : DomainEventBase(ProductId);

public record ProductMediaDeleted(Guid ProductId, Guid ProductMediaId) : DomainEventBase(ProductId);

public record ProductVariantQuantityUpdated(Guid ProductId, Guid VariantId, int Quantity) : DomainEventBase(ProductId);

public record ProductArchived(Guid ProductId) : DomainEventBase(ProductId);

public record ProductUnArchived(Guid ProductId, ProductStatus Status) : DomainEventBase(ProductId);

public record ProductDeleted(Guid ProductId) : DomainEventBase(ProductId);

public record ProductVariantPriceUpdated(Guid ProductId, Guid VariantId, decimal Price, decimal? CompareAtPrice, decimal? Cost) : DomainEventBase(ProductId);

public record ProductVariantBarcodeUpdated(Guid ProductId, Guid VariantId, string Barcode) : DomainEventBase(ProductId);

public record ProductVariantSkuUpdated(Guid ProductId, Guid VariantId, string Sku) : DomainEventBase(ProductId);

public record VariantRemoved(Guid ProductId, Guid VariantId) : DomainEventBase(ProductId);

public record ProductTurnedIntoSingleVariantProduct(Guid ProductId) : DomainEventBase(ProductId);

public record ProductMadeMultiVariant(Guid ProductId, IEnumerable<ProductOptionDto> Options) : DomainEventBase(ProductId);

public record VariantMediaChanged(Guid ProductId, Guid VariantId, Guid MediaId) : DomainEventBase(ProductId);