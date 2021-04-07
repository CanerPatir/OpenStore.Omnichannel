using System;
using System.Collections.Generic;
using System.Linq;
using OpenStore.Domain;
using OpenStore.Omnichannel.Domain.LookupContext;
using OpenStore.Omnichannel.Domain.ProductContext.Command;
using OpenStore.Omnichannel.Domain.ProductContext.Event;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable ReturnTypeCanBeEnumerable.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace OpenStore.Omnichannel.Domain.ProductContext
{
    public class Product : AggregateRoot<Guid>
    {
        private readonly HashSet<Variant> _variants = new();
        private readonly HashSet<SpecificationAttributeValue> _specificationAttributeValues = new();

        private readonly HashSet<ProductMedia> _medias = new();
        private readonly HashSet<ProductTag> _tags = new();

        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Sku { get; protected set; }

        public Guid CategoryId { get; protected set; }
        public virtual Category Category { get; protected set; }
        public Guid BrandId { get; protected set; }
        public virtual Brand Brand { get; protected set; }
        public virtual IReadOnlyCollection<Variant> Variants => _variants;
        public virtual IReadOnlyCollection<SpecificationAttributeValue> SpecificationAttributeValues => _specificationAttributeValues;

        public virtual IReadOnlyCollection<ProductMedia> Medias => _medias;
        public virtual IReadOnlyCollection<ProductTag> Tags => _tags;

        // todo: related products e.g: ikea items
        // todo: Product Family 
        
        protected Product()
        {
        }

        public static Product Create(Category category, Guid brandId, string name, string description)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            
            var product = new Product
            {
                Id =  Guid.NewGuid(),
                Category = category,
                BrandId = brandId,
                Name = name,
                Description = description,
            };
            product.ApplyChange(new ProductCreated(product.Id, product.CategoryId, product.BrandId, product.Name, product.Description));
            return product;
        }

        public static Product Create(Category category, CreateProductCommand command) 
            => Create(category, command.BrandId,  command.Name, command.Description);

        public void SetSpecificationAttribute(SetSpecificationAttributeToProductCommand command)
        {
            var categoryAttribute = Category.FindAttribute(command.AttributeId);
            var attribute = categoryAttribute.Attribute;

            if (attribute.Type == AttributeType.Text)
            {
                if (command.CustomValue == null)
                {
                    Fail(Msg.Domain.RequiredSpecificationAttributeValueNotFound);
                    return;
                }

                var productAttributeValue = SpecificationAttributeValues.SingleOrDefault(x => x.AttributeId == attribute.Id);

                if (productAttributeValue != null)
                {
                    if (string.Equals(command.CustomValue, productAttributeValue.CustomValue, StringComparison.InvariantCultureIgnoreCase))
                        return;

                    productAttributeValue.ChangeValue(command.CustomValue);
                }
                else
                {
                    _specificationAttributeValues.Add(new SpecificationAttributeValue(this, attribute, command.CustomValue));
                }
            }
            else
            {
                if (command.AttributeValueId == null)
                {
                    Fail(Msg.Domain.RequiredSpecificationAttributeValueNotFound);
                    return;
                }

                var attributeValue = attribute.FindAttributeValue(command.AttributeValueId.Value);
                var productAttributeValue = SpecificationAttributeValues.SingleOrDefault(x => x.AttributeId == attribute.Id);

                if (productAttributeValue != null)
                {
                    if (productAttributeValue.AttributeValueId == attributeValue.Id)
                        return;

                    productAttributeValue.ChangeValue(attributeValue);
                }
                else
                {
                    _specificationAttributeValues.Add(new SpecificationAttributeValue(this, attribute, attributeValue));
                }
            }

            ApplyChange(new ProductSpecificationAttributeSet(Id, command.AttributeId, command.AttributeValueId, command.CustomValue));
        }

        public void RemoveSpecificationAttribute(RemoveSpecificationAttributeCommand command)
        {
            var productAttributeValue = SpecificationAttributeValues.SingleOrDefault(x => x.AttributeId == command.AttributeId);

            if (productAttributeValue == null)
            {
                return;
            }

            _specificationAttributeValues.Remove(productAttributeValue);

            ApplyChange(new ProductSpecificationAttributeRemoved(Id, command.AttributeId));
        }

        public void AddMedia(string path, int? width, int? height)
        {
            _medias.Add(new ProductMedia(Id, path, width, height));

            ApplyChange(new MediaAdded(Id, path));
        }

        public void SetTag(Tag tag)
        {
            if (_tags.Add(new ProductTag(Id, tag.Id)))
            {
                ApplyChange(new TagAdded(Id, tag.Id, tag.Name));
            }
        }

        public void RemoveTag(Tag tag)
        {
            if (_tags.Remove(new ProductTag(Id, tag.Id)))
            {
                ApplyChange(new TagRemoved(Id, tag.Id));
            }
        }

        public Guid CreateVariant(CreateVariantCommand command)
        {
       
        }
    }
}