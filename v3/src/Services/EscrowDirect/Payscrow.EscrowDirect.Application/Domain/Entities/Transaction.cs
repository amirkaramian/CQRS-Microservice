using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Application.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public bool IsEscrow { get; set; }

        public decimal TotalChargeInclusive { get; set; }
        public decimal MerchantCharge { get; set; }
        public decimal CustomerCharge { get; set; }


        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; }


        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
