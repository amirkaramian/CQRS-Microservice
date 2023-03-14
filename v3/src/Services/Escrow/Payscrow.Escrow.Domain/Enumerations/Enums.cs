using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payscrow.Escrow.Domain.Enumerations
{
    public enum TransactionLocation
    {
        Escrow = 1,
        Wallet,
        Charges
    }

    public enum SettlementStatus
    {
        Pending = 0,
        Initiated = 1,
        Settled = 2
    }

    public enum TransactionType
    {
        Credit = 5,
        Debit = 10
    }

    public enum EscrowTransactionType
    {
        CodeRedemption = 1,
        TimeCountDown = 2
    }
}