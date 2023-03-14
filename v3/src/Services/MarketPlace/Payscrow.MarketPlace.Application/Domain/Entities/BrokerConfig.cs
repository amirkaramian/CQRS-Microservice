using Payscrow.MarketPlace.Application.Domain.Enumerations;
using System;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class BrokerConfig : AccountEntity, IAuditableEntity
    {
        public ChargeMethod ChargeMethod { get; set; }

        public decimal ChargeCap { get; set; }

        public decimal Percentage { get; set; }
        public decimal FixedRate { get; set; }

        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime? UpdateUtc { get; set; }

        public BrokerConfig()
        {
            CreateUtc = DateTime.Now;
        }
    }
}
