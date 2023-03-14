using Payscrow.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Application.IntegrationEvents.Publishing
{
    public class EscrowCodeAppliedEvent : IntegrationEvent
    {
        public EscrowCodeAppliedEvent(Guid tenantId, Guid transactionGuid)
            : base(tenantId)
        {
            TransactionGuid = transactionGuid;
        }

        public Guid TransactionGuid { get; }
    }
}