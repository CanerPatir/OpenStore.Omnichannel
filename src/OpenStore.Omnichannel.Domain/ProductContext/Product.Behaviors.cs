using System;
using System.Collections.Generic;
using System.Linq;
using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public partial class Product
    {
        public static Product Create(CreateProduct command, Func<Guid, ProductMedia> attacher)
        {
            var model = command.Model;
            var productOptions = new HashSet<ProductOption>();
            if (model.HasMultipleVariants)
            {
                if (model.Options is null || !model.Options.Any())
                {
                    throw new DomainException(Msg.Domain.Product.MultipleVariantProductMustHasOptions);
                }

                productOptions = model.Options.Select(x => new ProductOption(x.Name, x.Values)).ToHashSet();
            }

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Handle = model.Handle,
                Title = model.Title,
                Description = model.Description,
                HasMultipleVariants = model.HasMultipleVariants,
                Status = model.Status,
                MetaTitle = model.MetaTitle,
                MetaDescription = model.MetaDescription,
                Tags = model.Tags,

                IsPhysicalProduct = model.IsPhysicalProduct,
                Weight = model.Weight,
                WeightUnit = model.WeightUnit,
                HsCode = model.HsCode,

                _options = productOptions
            };
            product.ApplyChange(ProductCreated.From(product, model.Options));

            foreach (var variantModel in model.Variants)
            {
                product.AddVariant(variantModel);
            }

            foreach (var mediaModel in model.Medias)
            {
                product.AssignMedia(mediaModel, attacher);
            }

            return product;
        }

        public void AddVariant(VariantDto variantDto)
        {
            _variants.Add(
                new Variant(
                    Id, variantDto.Option1,
                    variantDto.Option2,
                    variantDto.Option3,
                    variantDto.Price,
                    variantDto.CompareAtPrice,
                    variantDto.Cost,
                    variantDto.CalculateTaxAdditionally,
                    variantDto.Quantity,
                    variantDto.Sku,
                    variantDto.Barcode,
                    variantDto.TrackQuantity,
                    variantDto.ContinueSellingWhenOutOfStock
                )
            );

            ApplyChange(new VariantAddedToProduct(Id, variantDto));
        }

        public void AssignMedia(ProductMediaDto productMediaDto, Func<Guid, ProductMedia> attacher)
        {
            if (attacher is null) throw new ArgumentNullException(nameof(attacher));

            var productMedia = attacher.Invoke(productMediaDto.Id);
            _medias.Add(productMedia);

            ApplyChange(new MediaAssignedToProduct(Id, productMediaDto));
        }

        public void AssignAttachedMedia(ProductMedia productMedia, ProductMediaDto productMediaDto)
        {
            _medias.Add(productMedia);

            ApplyChange(new MediaAssignedToProduct(Id, productMediaDto));
        }

        public void UpdateQuantity(UpdateProductVariantQuantity command)
        {
            var (variantId, quantity) = command;
            if (quantity < 0)
            {
                throw new DomainException(Msg.Domain.Product.QuantityShouldBeGreaterOrEqualThenZero);
            }

            var variant = _variants.SingleOrDefault(x => x.Id == variantId);
            if (variant is null)
            {
                throw new DomainException(Msg.ResourceNotFound);
            }

            variant.UpdateQuantity(quantity);

            ApplyChange(new ProductVariantQuantityUpdated(Id, variantId, quantity));
        }

        public void Archive()
        {
            if (Status == ProductStatus.Archived)
            {
                return;
            }

            Status = ProductStatus.Archived;
            ApplyChange(new ProductArchived(Id));
        }

        public void UnArchive()
        {
            if (Status != ProductStatus.Archived)
            {
                return;
            }

            Status = ProductStatus.Active;
            ApplyChange(new ProductUnArchived(Id, Status));
        }

        public void Delete()
        {
            if (SoftDeleted)
            {
                return;
            }

            SoftDeleted = true;
            ApplyChange(new ProductDeleted(Id));
        }

        public void UpdateProductMedias(UpdateProductMedias command)
        {
            foreach (var productMediaDto in command.Medias)
            {
                var productMedia = Medias.SingleOrDefault(x => x.Id == productMediaDto.Id);

                if (productMedia is null) continue;

                productMedia.Update(productMediaDto);
                ApplyChange(new ProductMediaUpdated(Id, productMediaDto));
            }
        }

        public void DeleteMedia(DeleteProductMedia command)
        {
            var productMedia = Medias.SingleOrDefault(x => x.Id == command.ProductMediaId);
            if (productMedia is null)
            {
                throw new DomainException(Msg.ResourceNotFound);
            }

            _medias.Remove(productMedia);
            ApplyChange(new ProductMediaDeleted(Id, command.ProductMediaId));
        }

        public void UpdateVariantPrices(UpdateVariantPrices command)
        {
            foreach (var updateVariantPriceCommand in command.Variants)
            {
                var variant = Variants.SingleOrDefault(v => v.Id == updateVariantPriceCommand.VariantId);
                if (variant is null)
                {
                    throw new DomainException(Msg.ResourceNotFound);
                }

                variant.UpdatePrice(updateVariantPriceCommand.Price, updateVariantPriceCommand.CompareAtPrice, updateVariantPriceCommand.Cost);
                ApplyChange(new VariantPriceUpdated(Id, variant.Id, variant.Price, variant.CompareAtPrice, variant.Cost));
            }
        }
    }
}