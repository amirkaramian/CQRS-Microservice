namespace Payscrow.Escrow.Domain.Enumerations
{
    public class EscrowTransactionRole : Enumeration
    {
        public static EscrowTransactionRole Customer = new EscrowTransactionRole(1, "Customer");
        public static EscrowTransactionRole Merchant = new EscrowTransactionRole(2, "Merchant");
        public static EscrowTransactionRole Broker = new EscrowTransactionRole(3, "Broker");

        protected EscrowTransactionRole(int id, string name) : base(id, name)
        {
        }
    }
}