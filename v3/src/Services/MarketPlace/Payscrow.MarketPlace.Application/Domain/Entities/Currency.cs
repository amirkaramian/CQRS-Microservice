using Payscrow.MarketPlace.Application.Domain.Enumerations;

namespace Payscrow.MarketPlace.Application.Domain.Entities
{
    public class Currency : Entity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }

        public ChargeMethod ChargeMethod { get; set; }

        public decimal ChargeCap { get; set; }
    }
}
