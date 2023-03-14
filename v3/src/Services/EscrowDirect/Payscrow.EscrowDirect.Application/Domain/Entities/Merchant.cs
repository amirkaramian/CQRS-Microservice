namespace Payscrow.EscrowDirect.Application.Domain.Entities
{
    public class Merchant : AuditableEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string LogoUrl { get; set; }
        public string ApiKey { get; set; }

        public decimal? CustomerChargePercentage { get; set; }


        public decimal ChargeFixedRate { get; set; }
        public decimal ChargePercentage { get; set; }
        public decimal ChargeCap { get; set; }
        public bool UseMerchantRates { get; set; }
    }
}
