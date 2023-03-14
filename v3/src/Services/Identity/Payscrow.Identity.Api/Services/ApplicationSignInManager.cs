using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Payscrow.Identity.Api.Data;
using Payscrow.Identity.Api.Models;
using System;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Services
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private readonly ApplicationIdentityDbContext _context;

        public ApplicationSignInManager(ApplicationUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<ApplicationSignInManager> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation, ApplicationIdentityDbContext context)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _context = context;
        }

        public async Task<SignInResult> PasswordSignInAsync(Guid tenantId, string username, string password, bool isPersistent, bool lockoutOnFailure)
        {
            username = $"{username}_{tenantId}";

            if (!await _context.Users.AnyAsync(x => x.UserName == username))
            {
                return SignInResult.Failed;
            }

            return await base.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure);
        }
    }
}