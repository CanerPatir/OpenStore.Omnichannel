// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public static class Msg
{
    private const string _ = "."; // discriminator char
    public const string OpenStoreGenericError = "OpenStore.GenericError";

    public static class Domain
    {
        public static class Product
        {
            public const string MultipleVariantProductMustHasOptions = nameof(Domain) + _ + nameof(MultipleVariantProductMustHasOptions);
            public const string ProductHandleAlreadyExists = nameof(Domain) + _ + nameof(ProductHandleAlreadyExists);
            public const string VariantStockIsNotTracking = nameof(Domain) + _ + nameof(VariantStockIsNotTracking);
            public const string QuantityShouldBeGreaterOrEqualThenZero = nameof(Domain) + _ + nameof(QuantityShouldBeGreaterOrEqualThenZero);
            public const string VariantAlreadyExistsThatHasSameOptions = nameof(Domain) + _ + nameof(VariantAlreadyExistsThatHasSameOptions);
            public const string MaxVariantLimitExceeded = nameof(Domain) + _ + nameof(MaxVariantLimitExceeded);
        }

        public static class ProductCollection
        {
            public const string CollectionNameIsRequired = nameof(Domain) + _ + nameof(CollectionNameIsRequired);
        }

        public static class Inventory
        {
            public const string QuantityShouldBeGreaterOrEqualThenZero = nameof(Domain) + _ + nameof(QuantityShouldBeGreaterOrEqualThenZero);
            public const string OutOfStock = nameof(Domain) + _ + nameof(OutOfStock);
        }

        public static class Checkout
        {
            public const string ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero = nameof(Domain) + _ + nameof(ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero);
            public const string ShoppingCartAlreadyContainsTheGivenVariant = nameof(Domain) + _ + nameof(ShoppingCartAlreadyContainsTheGivenVariant);
            public const string ShoppingCartItemNotFound = nameof(Domain) + _ + nameof(ShoppingCartItemNotFound);
        }
    }

    public static class Application
    {
        public const string UserNotFoundWithGivenEmail = nameof(Application) + _ + nameof(UserNotFoundWithGivenEmail);
        public const string InvalidToken = nameof(Application) + _ + nameof(InvalidToken);
        public const string PaymentError = nameof(Application) + _ + nameof(PaymentError);
        public const string PasswordChangeError = nameof(Application) + _ + nameof(PasswordChangeError);
        public const string InvalidLoginAttempt = nameof(Application) + _ + nameof(InvalidLoginAttempt);
        public const string CartNotExists = nameof(Application) + _ + nameof(CartNotExists);
        public const string CartNotCreatedYet = nameof(Application) + _ + nameof(CartNotCreatedYet);
    }

    public static class Validation
    {
        public const string Required = nameof(Validation) + _ + nameof(Required);
        public const string InvalidEmail = nameof(Validation) + _ + nameof(InvalidEmail);
        public const string PasswordMismatch = nameof(Validation) + _ + nameof(PasswordMismatch);
        public const string DuplicateEmail = nameof(Validation) + _ + nameof(DuplicateEmail);
        public const string PasswordTooShort = nameof(Validation) + _ + nameof(PasswordTooShort);
        public const string PasswordRequiresDigit = nameof(Validation) + _ + nameof(PasswordRequiresDigit);
        public const string Range = nameof(Validation) + _ + nameof(Range);
        public const string Regex = nameof(Validation) + _ + nameof(Regex);
        public const string InvalidCreditCard = nameof(Validation) + _ + nameof(InvalidCreditCard);
        public const string PasswordRequiresLower = nameof(Validation) + _ + nameof(PasswordRequiresLower);
        public const string PasswordRequiresUpper = nameof(Validation) + _ + nameof(PasswordRequiresUpper);
        public const string PasswordRequiresUniqueChars = nameof(Validation) + _ + nameof(PasswordRequiresUniqueChars);
        public const string PasswordRequiresNonAlphanumeric = nameof(Validation) + _ + nameof(PasswordRequiresNonAlphanumeric);
        public const string MaxLength = nameof(Validation) + _ + nameof(MaxLength);
    }

    public const string ResourceNotFound = nameof(ResourceNotFound);
}