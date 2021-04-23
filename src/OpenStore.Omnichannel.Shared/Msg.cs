// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel
{
    public static class Msg
    {
        private const string DiscriminatorChar = ".";
        public const string OpenStoreGenericError = "OpenStore.GenericError";

        public class Domain
        {
            public const string GivenAttrValueNotBelongAttr = nameof(Domain) + DiscriminatorChar + nameof(GivenAttrValueNotBelongAttr);
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
}