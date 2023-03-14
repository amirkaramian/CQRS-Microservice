using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Interfaces
{
    public interface IChargeCalculatorService
    {
        Task<decimal> GetChargeAsync(decimal amount, decimal percentage, string currencyCode);
    }
}
