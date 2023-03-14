using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.EscrowDirect.Application.Domain.Entities
{
    public class Item : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
