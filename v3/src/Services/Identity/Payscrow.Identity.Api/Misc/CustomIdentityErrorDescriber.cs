using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Payscrow.Identity.Api.Misc
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            var stripedEmail = StripeTenantIdFromEmail(email);

            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = $"Email {stripedEmail} is already taken."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            var stripedEmail = StripeTenantIdFromEmail(userName);

            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = $"Email {stripedEmail} is already taken."
            };
        }

        private string StripeTenantIdFromEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return string.Empty;
            var splitedString = email.Split('_', StringSplitOptions.RemoveEmptyEntries);

            return string.Concat(splitedString.Reverse().Skip(1).Reverse().ToArray());
        }
    }
}