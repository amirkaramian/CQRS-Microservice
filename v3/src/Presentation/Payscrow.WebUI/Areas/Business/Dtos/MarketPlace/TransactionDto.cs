using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.WebUI.Areas.Business.Dtos.MarketPlace
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public Guid BrokerAccountId { get; set; }
        public string BrokerTransactionReference { get; set; }
        public string BrokerName { get; set; }
        public decimal BrokerFee { get; set; }

        public Guid MerchantAccountId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantEmailAddress { get; set; }
        public decimal MerchantCharge { get; set; }

        public Guid? CustomerAccountId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public decimal CustomerCharge { get; set; }

        public decimal GrandTotalPayable { get; set; }

        public int StatusId { get; set; }
        public bool InEscrow { get; set; }
        public bool InDispute { get; set; }
        public string EscrowCode { get; set; }

        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }

        public Guid CreateUserId { get; set; }
        public DateTime CreateUtc { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime? UpdateUtc { get; set; }

        public string StatusName { get; set; }
        public string Actions { get; set; }
    }
}