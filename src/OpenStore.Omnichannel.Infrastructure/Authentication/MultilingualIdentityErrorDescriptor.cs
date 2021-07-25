using Microsoft.AspNetCore.Identity;
using OpenStore.Infrastructure.Localization;

namespace OpenStore.Omnichannel.Infrastructure.Authentication
{
    public class MultilingualIdentityErrorDescriptor : IdentityErrorDescriber
    {
        private readonly IOpenStoreLocalizer _openStoreLocalizer;

        public MultilingualIdentityErrorDescriptor(IOpenStoreLocalizer openStoreLocalizer)
        {
            _openStoreLocalizer = openStoreLocalizer;
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            var error = base.PasswordRequiresNonAlphanumeric();
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordRequiresNonAlphanumeric];
            return error;
        }

        public override IdentityError PasswordRequiresDigit()
        {
            var error = base.PasswordRequiresDigit();
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordRequiresDigit];
            return error;
        }

        public override IdentityError PasswordTooShort(int length)
        {
            var error = base.PasswordTooShort(length);
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordTooShort, length];
            return error;
        }

        public override IdentityError PasswordMismatch()
        {
            var error = base.PasswordMismatch();
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordMismatch];
            return error;
        }

        public override IdentityError PasswordRequiresLower()
        {
            var error = base.PasswordRequiresLower();
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordRequiresLower];
            return error;
        }

        public override IdentityError PasswordRequiresUpper()
        {
            var error = base.PasswordRequiresUpper();
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordRequiresUpper];
            return error;
        }

        public override IdentityError InvalidEmail(string email)
        {
            var error = base.InvalidEmail(email);
            error.Description = _openStoreLocalizer[Msg.Validation.InvalidEmail, email];
            return error;
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            var error = base.PasswordRequiresUniqueChars(uniqueChars);
            error.Description = _openStoreLocalizer[Msg.Validation.PasswordRequiresUniqueChars, uniqueChars];
            return error;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            var error = base.DuplicateEmail(email);
            error.Description = _openStoreLocalizer[Msg.Validation.DuplicateEmail, email];
            return error;
        }
    }
}