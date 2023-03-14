using Payscrow.PaymentInvite.Domain.SeedWork;
using System;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class TransactionItem : Entity
    {
        public string ImageFileName { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }


        public Guid? DealItemId { get; set; }
        public DealItem DealItem { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
