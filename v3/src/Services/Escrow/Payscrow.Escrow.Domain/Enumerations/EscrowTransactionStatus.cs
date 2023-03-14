namespace Payscrow.Escrow.Domain.Enumerations
{
    public class EscrowTransactionStatus : Enumeration
    {
        public static EscrowTransactionStatus InEscrow = new EscrowTransactionStatus(1, "In Escrow");
        public static EscrowTransactionStatus PendingSettlement = new EscrowTransactionStatus(2, "Pending Settlement");
        public static EscrowTransactionStatus InitiatedSettlement = new EscrowTransactionStatus(3, "Settlement Initiated");
        public static EscrowTransactionStatus CompletedSettlement = new EscrowTransactionStatus(4, "Completed Settlement");

        protected EscrowTransactionStatus(int id, string name) : base(id, name)
        {
        }
    }
}