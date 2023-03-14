using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Payscrow.Identity.Api.Data;
using Payscrow.Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationIdentityDbContext _context;

        public ProfileService(ApplicationUserManager userManager, ApplicationIdentityDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        async public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

            var user = await _userManager.FindByIdAsync(subjectId);

            if (user == null)
                throw new ArgumentException("Invalid subject identifier");

            var claims = await GetClaimsFromUser(user);
            context.IssuedClaims = claims.ToList();
        }

        async public Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

            var subjectId = subject.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            var user = await _userManager.FindByIdAsync(subjectId);

            context.IsActive = false;

            if (user != null)
            {
                if (_userManager.SupportsUserSecurityStamp)
                {
                    var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
                    if (security_stamp != null)
                    {
                        var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
                        if (db_security_stamp != security_stamp)
                            return;
                    }
                }

                context.IsActive =
                    !user.LockoutEnabled ||
                    !user.LockoutEnd.HasValue ||
                    user.LockoutEnd <= DateTime.Now;
            }
        }

        private async Task<IEnumerable<Claim>> GetClaimsFromUser(ApplicationUser user)
        {
            var account = await (from ua in _context.ApplicationUserAccounts
                                   join a in _context.Accounts
                                   on ua.AccountId equals a.Id
                                   where ua.UserId == user.Id
                                   select new { a.Id, a.Name }).FirstOrDefaultAsync();

            var username = user.UserName.Split("_")[0];

            var name = $"{user.FirstName} {user.LastName}";

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                new Claim("account_id", account.Id.ToString()),
                new Claim("account_name", account.Name),
                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(name) ? username : name),
                new Claim(JwtClaimTypes.PreferredUserName, username),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim("tenant_id", user.TenantId.ToString())
            };

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                claims.Add(new Claim("first_name", user.FirstName));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                claims.Add(new Claim("last_name", user.LastName));


            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                }
            }

            return claims;
        }
    }
}
