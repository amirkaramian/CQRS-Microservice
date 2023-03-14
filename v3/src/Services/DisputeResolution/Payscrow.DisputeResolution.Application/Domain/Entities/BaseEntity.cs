using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.DisputeResolution.Application.Domain.Entities
{
    public class BaseEntity
    {
        public Guid TransactionId { get; set; }// will be used as the foreignkey to some tables like DisputedTransaction, DisputedTransactionChatRecord etc
        public DateTime CreateUtc { get; set; }
    }
}
