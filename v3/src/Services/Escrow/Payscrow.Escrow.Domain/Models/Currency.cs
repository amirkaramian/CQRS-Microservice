namespace Payscrow.Escrow.Domain.Models
{
    public class Currency : Entity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public int Order { get; set; }
    }
}