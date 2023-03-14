using Microsoft.AspNetCore.Identity;
using Payscrow.Core.Bus;
using Payscrow.Identity.Api.Constants;
using Payscrow.Identity.Api.Models;
using Payscrow.Identity.Api.Services;
using System;

namespace Payscrow.Identity.Api.Data
{
    public static class IdentityUserAndRoleDataInitializer
    {
        public static void SeedData(ApplicationUserManager userManager, RoleManager<ApplicationRole> roleManager, ApplicationIdentityDbContext context, IEventBus eventBus)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, context, eventBus);
        }

        private static void SeedUsers(ApplicationUserManager userManager, ApplicationIdentityDbContext context, IEventBus eventBus)
        {
            if (userManager.FindByEmailAsync("ebimie@payscrow.net").Result == null)
            {
                ApplicationUser user = new ApplicationUser(Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"), "ebimie@payscrow.net", "Ebimie", "Abari")
                {
                    Email = "ebimie@payscrow.net",
                    PhoneNumber = "08037452476",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Swordfish1#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RolesConstants.ADMIN).Wait();
                    userManager.AddToRoleAsync(user, RolesConstants.SUPER_ADMIN).Wait();

                    var accountService = new AccountService(context, userManager, eventBus);
                    accountService.CreateAccountAsync(user).Wait();

                    context.SaveChanges();
                }
            }

            if (userManager.FindByEmailAsync("ekpedeme@payscrow.net").Result == null)
            {
                ApplicationUser user = new ApplicationUser(Guid.Parse("30867e39-acca-4565-b5e5-c3785b6f8899"), "ekpedeme@payscrow.net", "Ekpedeme", "Ekpe")
                {
                    Email = "ekpedeme@payscrow.net",
                    PhoneNumber = "08037452476",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Payscrow247*").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RolesConstants.ADMIN).Wait();

                    var accountService = new AccountService(context, userManager, eventBus);
                    accountService.CreateAccountAsync(user).Wait();

                    context.SaveChanges();
                }
            }
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RolesConstants.ADMIN).Result)
            {
                var role = new ApplicationRole
                {
                    Name = RolesConstants.ADMIN
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(RolesConstants.SUPER_ADMIN).Result)
            {
                var role = new ApplicationRole
                {
                    Name = RolesConstants.SUPER_ADMIN
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(RolesConstants.BUSINESS).Result)
            {
                var role = new ApplicationRole
                {
                    Name = RolesConstants.BUSINESS
                };
                _ = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(RolesConstants.PERSONAL).Result)
            {
                var role = new ApplicationRole
                {
                    Name = RolesConstants.PERSONAL
                };
                _ = roleManager.CreateAsync(role).Result;
            }
        }
    }
}