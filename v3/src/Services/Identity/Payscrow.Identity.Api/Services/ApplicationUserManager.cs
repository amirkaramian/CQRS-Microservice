using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payscrow.Identity.Api.Data;
using Payscrow.Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly ApplicationIdentityDbContext _context;

        public ApplicationUserManager(IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger, ApplicationIdentityDbContext context)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

        //public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        //{
        //    user.UserName = $"{user.UserName}_{user.TenantId}";

        //    if (await _context.Users.AnyAsync(x => x.UserName == user.UserName))
        //    {
        //        return IdentityResult.Failed(new[] { new IdentityError { Description = "Email already exist" } });
        //    }

        //    return await base.CreateAsync(user, password);
        //}

        //public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        //{
        //    if (await _context.Users.AnyAsync(x => x.UserName == user.UserName))
        //    {
        //        return IdentityResult.Failed(new[] { new IdentityError { Description = "Email already exist" } });
        //    }

        //    return await base.CreateAsync(user);
        //}

        //public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password, Guid tenantId)
        //{
        //    if (tenantId == default || tenantId == Guid.Empty)
        //    {
        //        throw new InvalidOperationException("Tenant ID cannot be empty!");
        //        //return IdentityResult.Failed(new IdentityError { Description = "Tenant ID cannot be empty" });
        //    }
        //    else
        //    {
        //        if (await _context.Users.AnyAsync(x => x.UserName == user.UserName))
        //        {
        //            return IdentityResult.Failed(new[] { new IdentityError { Description = "Email already exist" } });
        //        }

        //        return await base.CreateAsync(user, password);
        //    }
        //}

        public async Task<ApplicationUser> FindByNameAsync(string userName, Guid tenantId)
        {
            userName = $"{userName}_{tenantId}";

            var user = await base.FindByNameAsync(userName);

            if (user?.TenantId == tenantId) return user;
            else return null;
        }
    }
}