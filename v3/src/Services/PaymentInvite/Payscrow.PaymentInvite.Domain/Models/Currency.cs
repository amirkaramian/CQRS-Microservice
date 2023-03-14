using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.SeedWork;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class Currency : Entity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }

        public decimal PercentageCharge { get; set; }
        public decimal FixedCharge { get; set; }
        public ChargeType ChargeType { get; set; }
    }
}
