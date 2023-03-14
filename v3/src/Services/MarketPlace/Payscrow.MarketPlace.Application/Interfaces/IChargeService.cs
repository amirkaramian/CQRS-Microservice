using System;
using System.Threading.Tasks;

namespace Payscrow.MarketPlace.Application.Interfaces
{
    public interface IChargeService
    {
        Task<ChargeResult> CalculateChargesAsync(Guid tenantId, Guid currencyId, decimal amount, Guid? brokerAccountId = null);
    }

    public class ChargeResult
    {
        public decimal Charge { get; set; }
    }
}
