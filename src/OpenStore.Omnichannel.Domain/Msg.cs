namespace OpenStore.Omnichannel.Domain
{
    public static class Msg
    {
        private const string DiscriminatorChar = ".";

        public class Domain
        {
            public const string GivenAttrNotBelongProductCategory = nameof(Domain) + DiscriminatorChar + nameof(GivenAttrNotBelongProductCategory);
            public const string GivenAttrValueNotBelongAttr = nameof(Domain) + DiscriminatorChar + nameof(GivenAttrValueNotBelongAttr);
            public const string GivenAttributeIsNotGroupAttribute = nameof(Domain) + DiscriminatorChar + nameof(GivenAttributeIsNotGroupAttribute);
            public const string GivenAttributesNotEnoughToCreateVariant = nameof(Domain) + DiscriminatorChar + nameof(GivenAttributesNotEnoughToCreateVariant);
            public const string VariantWithSameAttributesExists = nameof(Domain) + DiscriminatorChar + nameof(VariantWithSameAttributesExists);
            public const string EmptyValueNotAllowedForVariantAttribute = nameof(Domain) + DiscriminatorChar + nameof(EmptyValueNotAllowedForVariantAttribute);
            public const string ProductWithSameAttributesExistsInSameGroup = nameof(Domain) + DiscriminatorChar + nameof(ProductWithSameAttributesExistsInSameGroup);
            public const string GivenAttributesNotEnoughToCreateProductGroup = nameof(Domain) + DiscriminatorChar + nameof(GivenAttributesNotEnoughToCreateProductGroup);
            public const string RequiredGroupAttributeValueNotFound = nameof(Domain) + DiscriminatorChar + nameof(RequiredGroupAttributeValueNotFound);
            public const string RequiredSpecificationAttributeValueNotFound = nameof(Domain) + DiscriminatorChar + nameof(RequiredSpecificationAttributeValueNotFound);
        }

        public const string ResourceNotFound = nameof(ResourceNotFound);
    }
}