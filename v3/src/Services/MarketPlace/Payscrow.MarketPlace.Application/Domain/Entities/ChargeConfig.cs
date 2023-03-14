using System;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class ChargeConfig : Entity
    {
        public decimal MaxTransactionAmount { get; set; }
        public decimal MinTransactionAmount { get; set; }
        
        public decimal Percentage { get; set; }
        public decimal FixedRate { get; set; }


        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
