namespace Payscrow.Escrow.Domain.Enumerations
{
    public class ServiceType : Enumeration
    {
        public static ServiceType MarketPlace = new ServiceType(1, "Market Place", "Brokers, Merchants And Customers", true);
        public static ServiceType ProjectMilestone = new ServiceType(2, "Project Milestone", "Project Owner, Vendor", false);

        public ServiceType(int id, string name, string description, bool canApplyEscrowCode) : base(id, name)
        {
            Description = description;
            CanApplyEscrowCode = canApplyEscrowCode;
        }

        public string Description { get; }
        public bool CanApplyEscrowCode { get; }
    }
}