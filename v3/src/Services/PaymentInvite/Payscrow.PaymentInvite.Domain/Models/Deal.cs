using Payscrow.PaymentInvite.Domain.Enumerations;
using Payscrow.PaymentInvite.Domain.SeedWork;
using Payscrow.PaymentInvite.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Payscrow.PaymentInvite.Domain.Models
{
    public class Deal : Entity
    {
        public Deal()
        {
            Items = new List<DealItem>();
            Transactions = new List<Transaction>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string SellerEmail { get; set; }
        public PhoneNumber SellerPhone { get; set; }
        public decimal SellerChargePercentage { get; set; }
        public string SellerVerificationCode { get; set; }

        public string BuyerLink { get; set; }

        public bool IsVerified { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public Guid? SellerId { get; set; }
        public Trader Seller { get; set; }


        public DealStatus Status { get; set; }



        public List<DealItem> Items { get; private set; }
        public List<Transaction> Transactions { get; private set; }


    }
}
