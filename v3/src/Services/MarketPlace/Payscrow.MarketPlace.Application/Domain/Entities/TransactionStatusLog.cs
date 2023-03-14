using System;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class TransactionStatusLog : Entity
    {
        public TransactionStatusLog()
        {
            CreateUtc = DateTime.UtcNow;
        }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public int StatusId { get; set; }
        public bool InDispute { get; set; }
        public bool InEscrow { get; set; }
        public string Note { get; set; }

        public DateTime CreateUtc { get; set; }
    }
}