using System;
using System.Collections.Generic;
using System.Linq;
using OpenStore.Domain;
using OpenStore.Omnichannel.Shared.Dto.Product;

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public partial class Product
    {
        private const int MaxVariantCount = 100;

        public static Product Create(CreateProduct command, Func<Guid, ProductMedia> attacher)
        {
            var model = command.Model;
            var productOptions = new List<ProductOption>();
            if (model.HasMultipleVariants)
            {
                if (model.Options is null || !model.Options.Any())
                {
                    throw new DomainException(Msg.Domain.Product.MultipleVariantProductMustHasOptions);
                }

                productOptions = model.Options.Select(x => new ProductOption(x.Name, x.Values.ToHashSet())).ToList();
            }

            var product = new Product
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

        private Variant AddVariant(VariantDto variantDto)
        {
            if (_variants.Count == MaxVariantCount)
            {
                throw new DomainException(Msg.Domain.Product.MaxVariantLimitExceeded);
            }

            var variant = new Variant(
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
            );
            _variants.Add(variant);

            ApplyChange(new VariantAddedToProduct(Id, variantDto));

            return variant;
        }

        public Variant CreateVariant(CreateVariant command)
        {
            var model = command.Model;
            if (Variants.Any(x => string.Equals(x.Option1, model.Option1, StringComparison.InvariantCultureIgnoreCase)
                                  && string.Equals(x.Option2, model.Option2, StringComparison.InvariantCultureIgnoreCase)
                                  && string.Equals(x.Option3, model.Option3, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new DomainException(Msg.Domain.Product.VariantAlreadyExistsThatHasSameOptions);
            }

            _options = _options.Select(x => x).ToList();

            if (model.Option1 is not null)
            {
                _options[0].Values.Add(model.Option1);
            }

            if (model.Option2 is not null)
            {
                _options[1].Values.Add(model.Option2);
            }

            if (model.Option3 is not null)
            {
                _options[2].Values.Add(model.Option3);
            }

            return AddVariant(model);
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

        public void UpdateVariantQuantities(UpdateProductVariantQuantities command)
        {
            foreach (var updateProductVariantQuantity in command.Variants)
            {
                UpdateVariantQuantity(updateProductVariantQuantity);
            }
        }

        public void UpdateVariantQuantity(UpdateProductVariantQuantity command)
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

        public void UpdateVariantPrices(UpdateProductVariantPrices command)
        {
            foreach (var updateVariantPriceCommand in command.Variants)
            {
                UpdateVariantPrice(updateVariantPriceCommand);
            }
        }

        public void UpdateVariantPrice(UpdateProductVariantPrice command)
        {
            var variant = Variants.SingleOrDefault(v => v.Id == command.VariantId);
            if (variant is null)
            {
                throw new DomainException(Msg.ResourceNotFound);
            }

            variant.UpdatePrice(command.Price, command.CompareAtPrice, command.Cost);
            ApplyChange(new ProductVariantPriceUpdated(Id, variant.Id, variant.Price, variant.CompareAtPrice, variant.Cost));
        }

        public void UpdateVariantBarcodes(UpdateProductVariantBarcodes command)
        {
            foreach (var updateVariantPriceCommand in command.Variants)
            {
                UpdateVariantBarcode(updateVariantPriceCommand);
            }
        }

        public void UpdateVariantBarcode(UpdateProductVariantBarcode command)
        {
            var variant = Variants.SingleOrDefault(v => v.Id == command.VariantId);
            if (variant is null)
            {
                throw new DomainException(Msg.ResourceNotFound);
            }

            variant.UpdateBarcode(command.Barcode);
            ApplyChange(new ProductVariantBarcodeUpdated(Id, variant.Id, variant.Barcode));
        }

        public void UpdateVariantSkus(UpdateProductVariantSkus command)
        {
            foreach (var updateVariantPriceCommand in command.Variants)
            {
                UpdateVariantSku(updateVariantPriceCommand);
            }
        }

        public void UpdateVariantSku(UpdateProductVariantSku command)
        {
            var variant = Variants.SingleOrDefault(v => v.Id == command.VariantId);
            if (variant is null)
            {
                throw new DomainException(Msg.ResourceNotFound);
            }

            variant.UpdateSku(command.Sku);
            ApplyChange(new ProductVariantSkuUpdated(Id, variant.Id, variant.Sku));
        }
    }
}