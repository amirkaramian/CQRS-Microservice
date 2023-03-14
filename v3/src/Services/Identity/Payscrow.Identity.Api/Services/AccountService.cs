using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Bus;
using Payscrow.Identity.Api.Data;
using Payscrow.Identity.Api.Dtos;
using Payscrow.Identity.Api.IntegrationEvents;
using Payscrow.Identity.Api.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payscrow.Identity.Api.Services
{
    public class AccountService
    {
        private readonly ApplicationIdentityDbContext _context;
        private readonly ApplicationUserManager _userManager;
        private readonly IEventBus _eventBus;

        public AccountService(ApplicationIdentityDbContext context, ApplicationUserManager userManager, IEventBus eventBus)
        {
            _context = context;
            _userManager = userManager;
            _eventBus = eventBus;
        }

        public async Task<AccountInformationDto> GetAccountInformationAsync(Guid userId, Guid tenantId)
        {
            var accountInfoDto = new AccountInformationDto();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            accountInfoDto.UserInfo.FullName = user.FullName;
            accountInfoDto.UserInfo.Email = user.Email;

            return accountInfoDto;
        }

        public async Task<Guid> CreateAccountAsync(ApplicationUser user)
        {
            var account = new Account(user.FullName.Trim())
            {
                Number = await GetNextAccountNumberAsync(),
                OwnerUserId = user.Id,
                CreateUserId = user.Id,
                UpdateUserId = user.Id,
                TenantId = user.TenantId,
                ApiKey = Guid.NewGuid().ToString()
            };

            _context.Accounts.Add(account);

            var userAccount = new ApplicationUserAccount
            {
                AccountId = account.Id,
                UserId = user.Id,
                CreateUserId = user.Id,
                UpdateUserId = user.Id,
                TenantId = user.TenantId
            };

            _context.ApplicationUserAccounts.Add(userAccount);

            return account.Id;
        }

        public async Task<AccountDto> GetAccountByApiKeyAsync(string apiKey, Guid tenantId)
        {
            return await _context.Accounts.ForTenant(tenantId).Where(x => x.ApiKey == apiKey)
                .Select(x => new AccountDto { Id = x.Id, Name = x.Name, Number = x.Number })
                .FirstOrDefaultAsync();
        }

        public async Task<AccountDto> QuickCreateOrGetAccountWithGeneratedPassword(QuickCreateAccountDto dto)
        {
            var result = new AccountDto();

            var user = await _userManager.FindByNameAsync(dto.EmailAddress, dto.TenantId);

            if (user != null)
            {
                result = await GetAccountByUserId(user.Id, dto.TenantId);
            }
            else
            {
                if (dto.TenantId == default || dto.TenantId == Guid.Empty)
                {
                    throw new InvalidOperationException("Tenant ID cannot be empty!");
                }

                user = new ApplicationUser(dto.TenantId, dto.EmailAddress, dto.Name, null) { Email = dto.EmailAddress, TenantId = dto.TenantId };
                var password = RandomStringHelper.GenerateRandomPassword(_userManager.Options.Password);
                var identityResult = await _userManager.CreateAsync(user, password);

                if (identityResult.Succeeded)
                {
                    result.Id = await CreateAccountAsync(user);
                    await _context.SaveChangesAsync();
                    var createdAccount = await _context.Accounts.FindAsync(result.Id);
                    result.Number = createdAccount.Number;

                    _eventBus.Publish(new UserRegisteredEvent(user.Id.ToString(),
                        createdAccount.Id.ToString(),
                        dto.TenantId.ToString(),
                        dto.EmailAddress,
                        dto.Name,
                        "",
                        dto.PhoneNumber,
                        true, password));
                }
            }

            return result;
        }

        public async Task<AccountDto> GetAccountByUserId(Guid userId, Guid tenantId)
        {
            return await (from u in _context.Users
                          join aua in _context.ApplicationUserAccounts on u.Id equals aua.UserId
                          join a in _context.Accounts.ForTenant(tenantId) on aua.AccountId equals a.Id
                          where u.Id == userId && u.TenantId == tenantId
                          select new AccountDto { Id = a.Id, Name = a.Name, Number = a.Number, IsVerified = u.EmailConfirmed }).SingleOrDefaultAsync();
        }

        public async Task<AccountDto> GetOrCreateAccountWithoutPassword(string emailAddress, Guid tenantId)
        {
            var result = new AccountDto();

            var user = await _userManager.FindByNameAsync(emailAddress, tenantId);

            if (user != null)
            {
                result = await GetAccountByUserId(user.Id, tenantId);
            }
            else
            {
                if (tenantId == default || tenantId == Guid.Empty)
                {
                    throw new InvalidOperationException("Tenant ID cannot be empty!");
                }

                user = new ApplicationUser(tenantId, emailAddress, null, null) { Email = emailAddress };
                var identityResult = await _userManager.CreateAsync(user);

                if (identityResult.Succeeded)
                {
                    result.Id = await CreateAccountAsync(user);
                    await _context.SaveChangesAsync();
                    var createdAccount = await _context.Accounts.FindAsync(result.Id);
                    result.Number = createdAccount.Number;
                }
            }

            return result;
        }

        /// <summary>
        /// Return the next account number to use when creating a new account. We add a random offset to
        /// the current highest account number.
        /// </summary>
        public async Task<long> GetNextAccountNumberAsync()
        {
            // Brand new site with no legacy data. Start from 10,000. Lowest possible account number is 10,001.
            const long INITIAL_ACCOUNT_NUMBER = 10_000L;

            // If this is the first account, start at INITIAL_ACCOUNT_NUMBER and add a random offset.
            var currentMaxNumber = await _context.Accounts
                .MaxAsync(ca => (long?)ca.Number) ?? INITIAL_ACCOUNT_NUMBER;

            return currentMaxNumber + GetRandomOffset();
        }

        private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(RandomHelper.CreateRandom);

        /// <summary>
        /// We don't want people to look at our numbers and be able to guess the number of customers we have, or to be
        /// able to sequentially guess each customer's account number, so generate a random offset to add to new
        /// account numbers.
        /// </summary>
        private long GetRandomOffset()
        {
            // The upper bound is exclusive, so this will return a random number between 1 and 9, inclusive.
            return _random.Value!.Next(1, 10);
        }
    }
}