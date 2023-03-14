using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.PaymentInvite.Domain.Enumerations
{
    //public enum TransactionStatus
    //{

    //}

    //public enum EscrowStatus
    //{
    //    Pending = 10,
    //    Escrowed = 20,
    //    Released = 30
    //}

    public enum DealStatus
    {
        Inactive = 1,
        Active,
        Closed,
        Expired
    }

    public enum PaymentStatus
    {
        Unpaid = 1,
        Paid,
        Refunded,
        PartiallyRefunded
    }

    public enum ChargeType
    {
        Percentage = 1,
        Fixed,
        Mixed
    }

    public enum NoteType
    {
        Transaction = 1
    }
}
