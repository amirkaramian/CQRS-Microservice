using Microsoft.EntityFrameworkCore;
using Payscrow.Core.Interfaces;
using Payscrow.MarketPlace.Application.Domain.Enumerations;
using Payscrow.MarketPlace.Application.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Services
{
    public class ChargeService : IChargeService
    {
        private readonly IMarketPlaceDbContext _context;

        public ChargeService(IMarketPlaceDbContext context)
        {
            _context = context;
        }

        public async Task<ChargeResult> CalculateChargesAsync(Guid tenantId, Guid currencyId, decimal amount, Guid? brokerAccountId = null)
        {
            var result = new ChargeResult();

            var currency = await _context.Currencies.ForTenant(tenantId).FirstOrDefaultAsync(x => x.IsActive && x.Id == currencyId);

            if (currency == null) return result;
            

            var chargeConfig = await _context.ChargeConfigs.ForTenant(tenantId).Where(x => x.MinTransactionAmount <= amount
                                        && x.MaxTransactionAmount >= amount)
                                        .FirstOrDefaultAsync();


            if (chargeConfig == null) return result;

            var chargeMethod = currency.ChargeMethod;
            var fixedRate = chargeConfig.FixedRate;
            var percentage = chargeConfig.Percentage;
            var chargeCap = currency.ChargeCap;


            if (brokerAccountId.HasValue)
            {
                var brokerConfig = await _context.BrokerConfigs.ForTenant(tenantId)
                    .SingleOrDefaultAsync(x => x.AccountId == brokerAccountId);

                if(brokerConfig != null)
                {
                    chargeMethod = brokerConfig.ChargeMethod;
                    fixedRate = brokerConfig.FixedRate;
                    percentage = brokerConfig.Percentage;
                    chargeCap = brokerConfig.ChargeCap;
                }
            }

            decimal finalCharge = 0;

            switch (chargeMethod)
            {
                case ChargeMethod.Percentage:
                finalCharge = amount * (percentage / 100);
                break;
                case ChargeMethod.FixedRate:
                finalCharge = fixedRate;
                break;
                case ChargeMethod.Combination:
                finalCharge = fixedRate + (amount * (percentage / 100));
                break;
            }

            if(finalCharge > chargeCap)
            {
                finalCharge = chargeCap;
            }

            result.Charge = finalCharge;

            return result;
        }
    }
}
