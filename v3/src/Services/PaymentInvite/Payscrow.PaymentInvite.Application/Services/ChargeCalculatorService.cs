using Microsoft.EntityFrameworkCore;
using Payscrow.PaymentInvite.Application.Common.Exceptions;
using Payscrow.PaymentInvite.Application.Interfaces;
using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Application.Services
{
    public class ChargeCalculatorService : IChargeCalculatorService, ITransientLifetime
    {
        private readonly IPaymentInviteDbContext _context;

        public ChargeCalculatorService(IPaymentInviteDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetChargeAsync(decimal amount, decimal percentage, string currencyCode)
        {
            var currency = await _context.Currencies.SingleOrDefaultAsync(x => x.Code == currencyCode);

            if (currency == null) throw new NotFoundException(nameof(Currency), currencyCode);

            var charge = currency.ChargeType switch
            {
                ChargeType.Percentage => (currency.PercentageCharge / 100) * amount,
                ChargeType.Fixed => currency.FixedCharge,
                ChargeType.Mixed => ((currency.PercentageCharge / 100) * amount) + currency.FixedCharge,
                _ => throw new Exception("invalid currency charge type!"),
            };

            return Math.Round((percentage / 100) * charge, 2);
        }
    }
}
