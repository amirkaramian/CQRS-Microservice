using Microsoft.EntityFrameworkCore;
using Payscrow.Escrow.Application.Interfaces;
using Payscrow.Escrow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.Services
{
    public class CurrencyService : ISelfTransientLifetime
    {
        private readonly IEscrowDbContext _context;
        private readonly ICacheManager _cache;

        public CurrencyService(IEscrowDbContext context, ICacheManager cache)
        {
            _context = context;
            _cache = cache;
        }

        private const string CURRENCIES_BY_ID_KEY = "payscrow.escrow.currency.id-{0}-{1}";
        private const string CURRENCIES_BY_CODE = "payscrow.escrow.currency.code-{0}-{1}";
        private const string CURRENCIES_ALL_KEY = "payscrow.escrow.currency-all-{0}";
        private const string CURRENCIES_PATTERN_KEY = "payscrow.escrow.currency.";

        public async Task<List<Currency>> GetAllCurrenciesAsync(Guid tenantId, bool IncludeInactive = false)
        {
            string key = string.Format(CURRENCIES_ALL_KEY, tenantId);

            return await _cache.GetAsync(key, 60, async () =>
            {
                var query = _context.Currencies.AsNoTracking().Where(x => x.TenantId == tenantId).AsQueryable();

                if (!IncludeInactive)
                    query = query.Where(x => x.IsActive);

                return await query.OrderByDescending(x => x.IsDefault).ThenByDescending(x => x.Order).ToListAsync();
            });
        }

        public async Task<Currency> GetCurrencyByIdAsync(Guid id, Guid tenantId)
        {
            string key = string.Format(CURRENCIES_BY_ID_KEY, id, tenantId);

            return await _cache.GetAsync(key, 60, async () =>
            {
                return await _context.Currencies.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId);
            });
        }

        public async Task<Currency> GetCurrencyByCodeAsync(string code, Guid tenantId)
        {
            string key = string.Format(CURRENCIES_BY_CODE, code, tenantId);

            return await _cache.GetAsync(key, 60, async () =>
            {
                return await _context.Currencies.FirstOrDefaultAsync(x => x.Code == code && x.TenantId == tenantId);
            });
        }

        public async Task<Guid?> GetCurrencyIdByCodeAsync(string code, Guid tenantId)
        {
            var currency = await GetCurrencyByCodeAsync(code, tenantId);
            return currency?.Id;
        }
    }
}