using System;

namespace Payscrow.Escrow.Domain.Models
{
    public class AccountSetting : Entity
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid? DefaultCurrencyId { get; set; }
        public Currency DefaultCurrency { get; set; }
    }
}