using Payscrow.PaymentInvite.Domain.SeedWork;
using System;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class DealItem : Entity
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public Guid DealId { get; set; }
        public Deal Deal { get; set; }
    }
}
