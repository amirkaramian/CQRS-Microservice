using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Services
{
    public class AccountSettingService : ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;
        private readonly ICacheManager _cache;

        public AccountSettingService(IEscrowDbContext context, ICacheManager cache)
        {
            _context = context;
            _cache = cache;
        }

        public const string SETTING_BY_ACCOUNT_ID = "payscrow.escrow.account.setting-{0}";

        public async Task<AccountSetting> GetAccountSettingAsync(Guid accountGuid, Guid tenantId, bool ignoreCache = false)
        {
            string key = string.Format(SETTING_BY_ACCOUNT_ID, accountGuid);

            return await _cache.GetAsync(key, 24 * 60, async () =>
            {
                var accountSetting = await (from a in _context.Accounts
                                            join acs in _context.AccountSettings on a.Id equals acs.AccountId
                                            where a.AccountGuid == accountGuid && a.TenantId == tenantId
                                            select acs)
                                          .FirstOrDefaultAsync();

                if (accountSetting == null)
                {
                    var account = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountGuid == accountGuid && x.TenantId == tenantId);
                    var defaultCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.IsDefault && x.IsActive && x.TenantId == tenantId);

                    if (account != null)
                    {
                        accountSetting = new AccountSetting
                        {
                            AccountId = account.Id,
                            TenantId = tenantId,
                            DefaultCurrencyId = defaultCurrency?.Id
                        };

                        _context.AccountSettings.Add(accountSetting);
                        await _context.SaveChangesAsync();
                    }
                }

                return accountSetting;
            }, ignoreCache);
        }

        public void RemoveAccountSettingFromCache(Guid accountGuid)
        {
            string key = string.Format(SETTING_BY_ACCOUNT_ID, accountGuid);

            _cache.Remove(key);
        }
    }
}