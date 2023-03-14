namespace Payscrow.Payments.Api.Application.Models
{
    public class SettlementInitiationResponseModel
    {
        public string GatewayReference { get; set; }
        public bool IsInitiated { get; set; }
    }
}