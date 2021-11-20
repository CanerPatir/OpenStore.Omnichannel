// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel;

public static class Msg
{
    private const string DiscriminatorChar = ".";
    public const string OpenStoreGenericError = "OpenStore.GenericError";

    public static class Domain
    {
        public static class Product
        {
            public const string MultipleVariantProductMustHasOptions = nameof(Domain) + DiscriminatorChar + nameof(MultipleVariantProductMustHasOptions);
            public const string ProductHandleAlreadyExists = nameof(Domain) + DiscriminatorChar + nameof(ProductHandleAlreadyExists);
            public const string VariantStockIsNotTracking = nameof(Domain) + DiscriminatorChar + nameof(VariantStockIsNotTracking);
            public const string QuantityShouldBeGreaterOrEqualThenZero = nameof(Domain) + DiscriminatorChar + nameof(QuantityShouldBeGreaterOrEqualThenZero);
            public const string VariantAlreadyExistsThatHasSameOptions = nameof(Domain) + DiscriminatorChar + nameof(VariantAlreadyExistsThatHasSameOptions);
            public const string MaxVariantLimitExceeded = nameof(Domain) + DiscriminatorChar + nameof(MaxVariantLimitExceeded);
        }

        public static class Inventory
        {
            public const string QuantityShouldBeGreaterOrEqualThenZero = nameof(Domain) + DiscriminatorChar + nameof(QuantityShouldBeGreaterOrEqualThenZero);
            public const string OutOfStock = nameof(Domain) + DiscriminatorChar + nameof(OutOfStock);
        }

        public static class Checkout
        {
            public const string ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero = nameof(Domain) + DiscriminatorChar + nameof(ShoppingCartItemQuantityShouldBeGreaterOrEqualThenZero);
            public const string ShoppingCartAlreadyContainsTheGivenVariant = nameof(Domain) + DiscriminatorChar + nameof(ShoppingCartAlreadyContainsTheGivenVariant);
            public const string ShoppingCartItemNotFound = nameof(Domain) + DiscriminatorChar + nameof(ShoppingCartItemNotFound);
        }
    }

    public static class Application
    {
        public const string UserNotFoundWithGivenEmail = nameof(Application) + DiscriminatorChar + nameof(UserNotFoundWithGivenEmail);
        public const string InvalidToken = nameof(Application) + DiscriminatorChar + nameof(InvalidToken);
        public const string PaymentError = nameof(Application) + DiscriminatorChar + nameof(PaymentError);
        public const string PasswordChangeError = nameof(Application) + DiscriminatorChar + nameof(PasswordChangeError);
        public const string InvalidLoginAttempt = nameof(Application) + DiscriminatorChar + nameof(InvalidLoginAttempt);
    }

    public static class Validation
    {
        public const string Required = nameof(Validation) + DiscriminatorChar + nameof(Required);
        public const string InvalidEmail = nameof(Validation) + DiscriminatorChar + nameof(InvalidEmail);
        public const string PasswordMismatch = nameof(Validation) + DiscriminatorChar + nameof(PasswordMismatch);
        public const string DuplicateEmail = nameof(Validation) + DiscriminatorChar + nameof(DuplicateEmail);
        public const string PasswordTooShort = nameof(Validation) + DiscriminatorChar + nameof(PasswordTooShort);
        public const string PasswordRequiresDigit = nameof(Validation) + DiscriminatorChar + nameof(PasswordRequiresDigit);
        public const string Range = nameof(Validation) + DiscriminatorChar + nameof(Range);
        public const string Regex = nameof(Validation) + DiscriminatorChar + nameof(Regex);
        public const string InvalidCreditCard = nameof(Validation) + DiscriminatorChar + nameof(InvalidCreditCard);
        public const string PasswordRequiresLower = nameof(Validation) + DiscriminatorChar + nameof(PasswordRequiresLower);
        public const string PasswordRequiresUpper = nameof(Validation) + DiscriminatorChar + nameof(PasswordRequiresUpper);
        public const string PasswordRequiresUniqueChars = nameof(Validation) + DiscriminatorChar + nameof(PasswordRequiresUniqueChars);
        public const string PasswordRequiresNonAlphanumeric = nameof(Validation) + DiscriminatorChar + nameof(PasswordRequiresNonAlphanumeric);
        public const string MaxLength = nameof(Validation) + DiscriminatorChar + nameof(MaxLength);
    }

    public const string ResourceNotFound = nameof(ResourceNotFound);
}