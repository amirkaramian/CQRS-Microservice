using System;

namespace Payscrow.Escrow.Domain.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string DomainName { get; set; }


        public DateTime CreateUtc { get; set; }
        public DateTime UpdateUtc { get; set; }


        public Tenant()
        {
            Id = SequentialGuid.GenerateComb();

            var utcNow = DateTime.UtcNow;
            CreateUtc = utcNow;
            UpdateUtc = utcNow;
        }
    }
}
