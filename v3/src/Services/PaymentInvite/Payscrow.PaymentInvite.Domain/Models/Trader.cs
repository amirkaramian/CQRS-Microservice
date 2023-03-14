using System;
using System.Collections.Generic;
using Payscrow.PaymentInvite.Domain.SeedWork;
using Payscrow.PaymentInvite.Domain.ValueObjects;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class Trader : Entity
    {
        public Trader()
        {
            Transactions = new HashSet<Transaction>();
            Deals = new HashSet<Deal>();
        }


        public string Name { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Address ContactAddress { get; set; }

        public ICollection<Transaction> Transactions { get; private set; }
        public ICollection<Deal> Deals { get; private set; }
    }
}
