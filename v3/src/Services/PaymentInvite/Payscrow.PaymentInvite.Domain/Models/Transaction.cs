using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.SeedWork;
using Payscrow.PaymentInvite.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class Transaction : Entity
    {
        public Transaction()
        {
            Items = new List<TransactionItem>();
            Notes = new List<Note>();
            PaymentStatus = PaymentStatus.Unpaid;
        }

        public long Number { get; set; }



        public int StatusId { get; set; }
        public TransactionStatus Status { get; private set; }

        public PaymentStatus PaymentStatus { get; set; }

        public bool InEscrow { get; set; }
        //public DateTime? PaymentDate { get; set; }


        public string BuyerEmail { get; set; }
        public PhoneNumber BuyerPhone { get; set; }
        public Address DeliveryAddress { get; set; }


        public Guid? BuyerId { get; set; }
        public Trader Buyer { get; set; }


        public Guid DealId { get; set; }
        public Deal Deal { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }


        public decimal TotalAmount { get; set; }
        public decimal SellerChargeAmount { get; set; }
        public decimal BuyerChargeAmount { get; set; }


        public List<TransactionItem> Items { get; private set; }
        public List<Note> Notes { get; private set; }

        
    }
}
