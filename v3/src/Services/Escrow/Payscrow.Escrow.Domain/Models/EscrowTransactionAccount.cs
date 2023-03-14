using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Domain.Models
{
    public class EscrowTransactionAccount : Entity
    {
        public int EscrowTransactionRoleId { get; set; }
        public Guid EscrowTransactionId { get; set; }
        public EscrowTransaction EscrowTransaction { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}