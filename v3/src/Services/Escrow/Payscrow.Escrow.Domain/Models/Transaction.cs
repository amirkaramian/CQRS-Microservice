using Payscrow.Escrow.Domain.Enumerations;
using System;

namespace Payscrow.Escrow.Domain.Models
{
    public class Transaction : Entity
    {
        protected Transaction() { }

        public Transaction(TransactionType type, decimal amount, TransactionLocation location, string description, Guid accountId, Guid currencyId)
        {
            Type = type;

            switch (type)
            {
                case TransactionType.Credit:
                Amount = (amount * amount) / amount;
                break;
                case TransactionType.Debit:
                Amount = amount < 0 ? amount : -1 * amount;
                break;
            }

            Description = description;
            Location = location;
            SourceAccountId = accountId;
            CurrencyId = currencyId;
        }


        public decimal Amount { get; set; }
        public string Description { get; set; }

        public TransactionType Type { get; set; }
        public TransactionLocation Location { get; set; }

        public Guid SourceAccountId { get; set; }
        public Account SourceAccount { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
